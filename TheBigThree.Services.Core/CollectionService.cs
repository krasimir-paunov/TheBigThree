using Microsoft.EntityFrameworkCore;
using TheBigThree.Contracts;
using TheBigThree.Data;
using TheBigThree.Data.Models;
using TheBigThree.Web.ViewModels;

namespace TheBigThree.Services;

public class CollectionService : ICollectionService
{
    private readonly TheBigThreeDbContext dbContext;

    public CollectionService(TheBigThreeDbContext dbContext)
    {
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

    public async Task<IEnumerable<CollectionAllViewModel>> GetAllCollectionsAsync(string? sorting = null)
    {
        IQueryable<Collection> collectionsQuery = dbContext.Collections.AsQueryable();

        collectionsQuery = sorting switch
        {
            "Stars" => collectionsQuery.OrderByDescending(c => c.TotalStars),
            "Newest" => collectionsQuery.OrderByDescending(c => c.CreatedOn),
            _ => collectionsQuery.OrderByDescending(c => c.CreatedOn)
        };

        List<CollectionAllViewModel> hubCollections = await collectionsQuery
                .AsNoTracking()
                .Select(c => new CollectionAllViewModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    Publisher = c.User.UserName ?? "Unknown",
                    TotalStars = c.TotalStars,
                    GameImages = c.Games.Select(g => g.ImageUrl).ToList(),
                    PublisherRank = ""
                })
                .ToListAsync();

        foreach (CollectionAllViewModel collection in hubCollections)
        {
            collection.Publisher = collection.Publisher.Split('@')[0];
            collection.PublisherRank = CalculateRank(collection.TotalStars);
        }

        return hubCollections;
    }

    public async Task<IEnumerable<CollectionAllViewModel>> GetMineCollectionsAsync(string userId)
    {
        List<CollectionAllViewModel> personalCollections = await dbContext.Collections
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .Select(c => new CollectionAllViewModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    Publisher = c.User.UserName ?? "Unknown",
                    TotalStars = c.TotalStars,
                    GameImages = c.Games.Select(g => g.ImageUrl).ToList(),
                    PublisherRank = ""
                })
                .ToListAsync();

        foreach (CollectionAllViewModel collection in personalCollections)
        {
            collection.Publisher = collection.Publisher.Split('@')[0];
            collection.PublisherRank = CalculateRank(collection.TotalStars);
        }

        return personalCollections;
    }

    public async Task<CollectionFormViewModel> GetNewAddFormModelAsync()
    {
        List<GenreSelectViewModel> availableGenres = await dbContext.Genres
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
        if (await dbContext.Collections.AnyAsync(c => c.UserId == userId))
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
            await dbContext.Games.AddAsync(newGame);
        }

        await dbContext.Collections.AddAsync(newlyCreated);
        await dbContext.SaveChangesAsync();
    }

    public async Task<CollectionDetailsViewModel?> GetCollectionDetailsByIdAsync(int id)
    {
        CollectionDetailsViewModel? detailedView = await dbContext.Collections
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
                }).ToList()
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
        bool isAlreadyCreated = await dbContext.Collections.AnyAsync(c => c.UserId == userId);
        return isAlreadyCreated;
    }

    public async Task<CollectionFormViewModel?> GetCollectionForEditAsync(int id, string userId)
    {
        Collection? collectionToEdit = await dbContext.Collections
            .Include(c => c.Games)
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

        if (collectionToEdit == null) return null;

        List<GenreSelectViewModel> genreList = await dbContext.Genres
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

    public async Task EditCollectionAsync(CollectionFormViewModel updatedData, int id)
    {
        Collection? existingCollection = await dbContext.Collections
                .Include(c => c.Games)
                .FirstOrDefaultAsync(c => c.Id == id);

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

        await dbContext.SaveChangesAsync();
    }

    public async Task<CollectionDetailsViewModel?> GetCollectionForDeleteAsync(int id, string userId)
    {
        CollectionDetailsViewModel? deletionPreview = await GetCollectionDetailsByIdAsync(id);
        return deletionPreview;
    }

    public async Task DeleteCollectionAsync(int id, string userId)
    {
        Collection? targetCollection = await dbContext.Collections
            .Include(c => c.Games)
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

        if (targetCollection != null)
        {
            List<Like> relatedStars = await dbContext.Likes
                .Where(l => l.CollectionId == id)
                .ToListAsync();

            if (relatedStars.Any()) dbContext.Likes.RemoveRange(relatedStars);
            dbContext.Games.RemoveRange(targetCollection.Games);
            dbContext.Collections.Remove(targetCollection);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task StarCollectionAsync(int collectionId, string userId)
    {
        Collection? targetCollection = await dbContext.Collections.FirstOrDefaultAsync(c => c.Id == collectionId);

        if (targetCollection == null) throw new ArgumentException("Collection does not exist.");
        if (targetCollection.UserId == userId) throw new InvalidOperationException("You cannot star your own collection.");

        bool isPreviouslyStarred = await dbContext.Likes.AnyAsync(l => l.UserId == userId && l.CollectionId == collectionId);
        if (isPreviouslyStarred) throw new InvalidOperationException("You have already starred this collection.");

        Like starEntry = new Like { UserId = userId, CollectionId = collectionId };
        await dbContext.Likes.AddAsync(starEntry);

        targetCollection.TotalStars++;
        await dbContext.SaveChangesAsync();
    }

    public async Task RemoveStarAsync(int collectionId, string userId)
    {
        Like? existingStar = await dbContext.Likes.FirstOrDefaultAsync(l => l.UserId == userId && l.CollectionId == collectionId);
        if (existingStar == null) return;

        Collection? targetCollection = await dbContext.Collections.FindAsync(collectionId);
        if (targetCollection != null && targetCollection.TotalStars > 0) targetCollection.TotalStars--;

        dbContext.Likes.Remove(existingStar);
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> IsStarredByUserAsync(int collectionId, string userId)
    {
        bool isStarred = await dbContext.Likes.AnyAsync(l => l.UserId == userId && l.CollectionId == collectionId);
        return isStarred;
    }

    public async Task<IEnumerable<CollectionAllViewModel>> GetStarredCollectionsAsync(string userId)
    {
        List<CollectionAllViewModel> favoriteCollections = await dbContext.Likes
            .Where(l => l.UserId == userId)
            .Select(l => l.Collection)
            .Select(c => new CollectionAllViewModel
            {
                Id = c.Id,
                Title = c.Title,
                Publisher = c.User.UserName ?? "Unknown",
                TotalStars = c.TotalStars,
                GameImages = c.Games.Select(g => g.ImageUrl).ToList(),
                PublisherRank = ""
            })
            .ToListAsync();

        foreach (CollectionAllViewModel collection in favoriteCollections)
        {
            collection.Publisher = collection.Publisher.Split('@')[0];
            collection.PublisherRank = CalculateRank(collection.TotalStars);
        }

        return favoriteCollections;
    }

    public async Task<int> GetUserTotalStarsAsync(string userId)
    {
        int totalStarsCount = await dbContext.Collections
            .Where(c => c.UserId == userId)
            .Select(c => c.TotalStars)
            .FirstOrDefaultAsync();

        return totalStarsCount;
    }
}