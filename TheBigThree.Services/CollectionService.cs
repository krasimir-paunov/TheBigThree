using Microsoft.EntityFrameworkCore;
using TheBigThree.Data;
using TheBigThree.Data.Models;
using TheBigThree.Services.Core.Interfaces;
using TheBigThree.Services.Core.Repositories;
using TheBigThree.Web.ViewModels;

namespace TheBigThree.Services;

public class CollectionService : ICollectionService
{
    private readonly IRepository<Collection> collectionRepository;
    private readonly IRepository<Game> gameRepository;
    private readonly IRepository<Like> likeRepository;
    private readonly IRepository<Genre> genreRepository;
    private readonly TheBigThreeDbContext dbContext;

    public CollectionService(
        IRepository<Collection> collectionRepository,
        IRepository<Game> gameRepository,
        IRepository<Like> likeRepository,
        IRepository<Genre> genreRepository,
        TheBigThreeDbContext dbContext)
    {
        this.collectionRepository = collectionRepository;
        this.gameRepository = gameRepository;
        this.likeRepository = likeRepository;
        this.genreRepository = genreRepository;
        this.dbContext = dbContext;
    }

    private string CalculateRank(int stars) => stars switch
    {
        >= 100 => "Legendary Collector",
        >= 30 => "Superstar Collector",
        >= 10 => "Popular Collector",
        >= 5 => "Rising Star",
        >= 1 => "Novice Collector",
        _ => "Newcomer"
    };

    public async Task<CollectionQueryModel> GetAllCollectionsAsync(CollectionQueryModel query)
    {
        IQueryable<Collection> collectionsQuery = collectionRepository.All();

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            string searchLower = query.SearchTerm.ToLower();

            collectionsQuery = collectionsQuery.Where(c =>
                c.Title.ToLower().Contains(searchLower) ||
                c.Games.Any(g => g.Title.ToLower().Contains(searchLower)));
        }

        if (!string.IsNullOrWhiteSpace(query.GenreFilter))
        {
            collectionsQuery = collectionsQuery.Where(c => c.Games.Any(g => g.Genre.Name == query.GenreFilter));
        }

        collectionsQuery = query.Sorting switch
        {
            "Stars" => collectionsQuery.OrderByDescending(c => c.TotalStars),
            _ => collectionsQuery.OrderByDescending(c => c.CreatedOn)
        };

        query.TotalCollections = await collectionsQuery.CountAsync();

        List<CollectionAllViewModel> hubCollections = await collectionsQuery
            .Skip((query.CurrentPage - 1) * query.CollectionsPerPage)
            .Take(query.CollectionsPerPage)
            .AsNoTracking()
            .Select(c => new CollectionAllViewModel
            {
                Id = c.Id,
                Title = c.Title,
                Publisher = c.User.UserName ?? "Unknown",
                TotalStars = c.TotalStars,
                GameImages = c.Games.Select(g => g.ImageUrl).ToList(),
                PublisherRank = "",
                AvatarUrl = dbContext.Users.OfType<ApplicationUser>()
                    .Where(u => u.Id == c.UserId)
                    .Select(u => u.AvatarUrl)
                    .FirstOrDefault()
            })
            .ToListAsync();

        foreach (CollectionAllViewModel collection in hubCollections)
        {
            collection.PublisherRank = CalculateRank(collection.TotalStars);
        }

        query.Genres = await genreRepository.All()
            .Select(g => g.Name)
            .OrderBy(g => g)
            .ToListAsync();

        query.Collections = hubCollections;
        return query;
    }

    public async Task<IEnumerable<CollectionAllViewModel>> GetMineCollectionsAsync(string userId)
    {
        List<CollectionAllViewModel> personalCollections = await collectionRepository.All()
                        .AsNoTracking()
                        .Where(c => c.UserId == userId)
                        .Select(c => new CollectionAllViewModel
                        {
                            Id = c.Id,
                            Title = c.Title,
                            Publisher = c.User.UserName ?? "Unknown",
                            TotalStars = c.TotalStars,
                            GameImages = c.Games.Select(g => g.ImageUrl).ToList(),
                            PublisherRank = "",
                            AvatarUrl = dbContext.Users.OfType<ApplicationUser>()
                        .Where(u => u.Id == c.UserId)
                        .Select(u => u.AvatarUrl)
                        .FirstOrDefault()
                        })
                .ToListAsync();

        foreach (CollectionAllViewModel collection in personalCollections)
        {
            collection.Publisher = collection.Publisher.Split('@')[0];
            collection.PublisherRank = CalculateRank(collection.TotalStars);
        }

        return personalCollections;
    }

    public async Task<CollectionMineViewModel?> GetMineCollectionAsync(string userId)
    {
        CollectionMineViewModel? collection = await collectionRepository.All()
            .AsNoTracking()
            .Where(c => c.UserId == userId)
            .Select(c => new CollectionMineViewModel
            {
                Id = c.Id,
                Title = c.Title,
                TotalStars = c.TotalStars,
                Games = c.Games.Select(g => new GameDetailsViewModel
                {
                    Title = g.Title,
                    ImageUrl = g.ImageUrl,
                    Description = g.Description,
                    Genre = g.Genre.Name
                }).ToList()
            })
            .FirstOrDefaultAsync();

        return collection;
    }

    public async Task<CollectionFormViewModel> GetNewAddFormModelAsync()
    {
        List<GenreSelectViewModel> availableGenres = await genreRepository.All()
            .Select(g => new GenreSelectViewModel { Id = g.Id, Name = g.Name })
            .ToListAsync();

        CollectionFormViewModel blankForm = new CollectionFormViewModel();

        for (int i = 0; i < 3; i++)
        {
            blankForm.Games.Add(new GameFormViewModel { Genres = availableGenres });
        }

        return blankForm;
    }

    public async Task AddCollectionAsync(CollectionFormViewModel inputData, string userId)
    {
        if (await collectionRepository.All().AnyAsync(c => c.UserId == userId))
        {
            throw new InvalidOperationException("You already have a collection!");
        }

        Collection newlyCreated = new Collection()
        {
            Title = inputData.Title,
            UserId = userId,
            CreatedOn = DateTime.UtcNow
        };

        foreach (GameFormViewModel gameEntry in inputData.Games)
        {
            Game newGame = new Game()
            {
                Title = gameEntry.Title,
                Description = gameEntry.Description,
                ImageUrl = gameEntry.ImageUrl,
                GenreId = gameEntry.GenreId,
                Collection = newlyCreated
            };

            await gameRepository.AddAsync(newGame);
        }

        await collectionRepository.AddAsync(newlyCreated);

        await collectionRepository.SaveChangesAsync();
    }

    public async Task<CollectionDetailsViewModel?> GetCollectionDetailsByIdAsync(int id)
    {
        CollectionDetailsViewModel? detailedView = await collectionRepository.All()
            .Where(c => c.Id == id)
            .Select(c => new CollectionDetailsViewModel
            {
                Id = c.Id,
                Title = c.Title,
                Publisher = c.User.UserName ?? "Unknown",
                TotalStars = c.TotalStars,
                Games = c.Games.Select(g => new GameDetailsViewModel
                {
                    Title = g.Title,
                    Description = g.Description,
                    ImageUrl = g.ImageUrl,
                    Genre = g.Genre.Name
                }).ToList(),
                AvatarUrl = dbContext.Users.OfType<ApplicationUser>()
                    .Where(u => u.Id == c.UserId)
                    .Select(u => u.AvatarUrl)
                    .FirstOrDefault()
            })
            .FirstOrDefaultAsync();

        if (detailedView != null)
        {
            detailedView.Publisher = detailedView.Publisher.Split('@')[0];
        }

        return detailedView;
    }

    public async Task<bool> UserHasCollectionAsync(string userId)
    {
        bool isAlreadyCreated = await collectionRepository.All()
            .AnyAsync(c => c.UserId == userId);

        return isAlreadyCreated;
    }

    public async Task<CollectionFormViewModel?> GetCollectionForEditAsync(int id, string userId)
    {
        Collection? collectionToEdit = await collectionRepository.All()
            .Include(c => c.Games)
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

        if (collectionToEdit == null) return null;

        List<GenreSelectViewModel> genreList = await genreRepository.All()
            .Select(g => new GenreSelectViewModel { Id = g.Id, Name = g.Name })
            .ToListAsync();

        return new CollectionFormViewModel
        {
            Title = collectionToEdit.Title,
            Games = collectionToEdit.Games.Select(g => new GameFormViewModel
            {
                Title = g.Title,
                ImageUrl = g.ImageUrl,
                Description = g.Description,
                GenreId = g.GenreId,
                Genres = genreList
            }).ToList()
        };
    }

    public async Task EditCollectionAsync(CollectionFormViewModel updatedData, int id, string userId)
    {
        Collection? existingCollection = await collectionRepository.All()
            .Include(c => c.Games)
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

        if (existingCollection == null) throw new ArgumentException("Collection not found.");

        existingCollection.Title = updatedData.Title;

        for (int i = 0; i < existingCollection.Games.Count; i++)
        {
            Game gameToUpdate = existingCollection.Games.ElementAt(i);

            GameFormViewModel updatedInfo = updatedData.Games[i];

            gameToUpdate.Title = updatedInfo.Title;
            gameToUpdate.Description = updatedInfo.Description;
            gameToUpdate.ImageUrl = updatedInfo.ImageUrl;
            gameToUpdate.GenreId = updatedInfo.GenreId;
        }

        await collectionRepository.SaveChangesAsync();
    }

    public async Task<CollectionDetailsViewModel?> GetCollectionForDeleteAsync(int id, string userId)
    {
        Collection? collectionToDelete = await collectionRepository.All()
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

        if (collectionToDelete == null) return null;

        CollectionDetailsViewModel? deletionPreview = await GetCollectionDetailsByIdAsync(id);
        return deletionPreview;
    }

    public async Task DeleteCollectionAsync(int id, string userId)
    {
        Collection? targetCollection = await collectionRepository.All()
            .Include(c => c.Games)
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

        if (targetCollection != null)
        {
            List<Like> relatedStars = await likeRepository.All()
                .Where(l => l.CollectionId == id)
                .ToListAsync();

            if (relatedStars.Any())
            {
                foreach (Like like in relatedStars)
                {
                    dbContext.Set<Like>().Remove(like);
                }
            }

            foreach (Game game in targetCollection.Games)
            {
                await gameRepository.DeleteAsync(game.Id);
            }

            await collectionRepository.DeleteAsync(id);

            await collectionRepository.SaveChangesAsync();
        }
    }
}