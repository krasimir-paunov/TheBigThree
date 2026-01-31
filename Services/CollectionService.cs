using Microsoft.EntityFrameworkCore;
using TheBigThree.Contracts;
using TheBigThree.Data;
using TheBigThree.ViewModels;
using TheBigThree.Models;

namespace TheBigThree.Services;

public class CollectionService : ICollectionService
{
    private readonly TheBigThreeDbContext dbContext;

    public CollectionService(TheBigThreeDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<CollectionAllViewModel>> GetAllCollectionsAsync()
    {
        var collections = await dbContext.Collections
                .AsNoTracking()
                .Select(c => new
                {
                    c.Id,
                    c.Title,
                    c.User.UserName,
                    c.TotalStars,
                    GameImages = c.Games.Select(g => g.ImageUrl).ToList()
                })
                .ToListAsync();

        return collections.Select(c => new CollectionAllViewModel
        {
            Id = c.Id,
            Title = c.Title,
            Publisher = c.UserName!.Split('@')[0],
            TotalStars = c.TotalStars,
            GameImages = c.GameImages
        });
    }

    public async Task<IEnumerable<CollectionAllViewModel>> GetMineCollectionsAsync(string userId)
    {
        var collectionsData = await dbContext.Collections
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .Select(c => new
                {
                    c.Id,
                    c.Title,
                    c.User.UserName,
                    c.TotalStars,
                    GameImages = c.Games.Select(g => g.ImageUrl).ToList()
                })
                .ToListAsync();

        return collectionsData.Select(c => new CollectionAllViewModel
        {
            Id = c.Id,
            Title = c.Title,
            Publisher = c.UserName?.Split('@')[0] ?? "Unknown",
            TotalStars = c.TotalStars,
            GameImages = c.GameImages
        });
    }

    public async Task<CollectionFormViewModel> GetNewAddFormModelAsync()
    {
        var genres = await dbContext.Genres
            .Select(g => new GenreSelectViewModel { Id = g.Id, Name = g.Name })
            .ToListAsync();

        var model = new CollectionFormViewModel();

        for (int i = 0; i < 3; i++)
        {
            model.Games.Add(new GameFormViewModel { Genres = genres });
        }

        return model;
    }

    public async Task AddCollectionAsync(CollectionFormViewModel model, string userId)
    {
        var collection = new Collection()
        {
            Title = model.Title,
            UserId = userId,
        };

        foreach (var gameModel in model.Games)
        {
            var game = new Game()
            {
                Title = gameModel.Title,
                Description = gameModel.Description,
                ImageUrl = gameModel.ImageUrl,
                GenreId = gameModel.GenreId,
                Collection = collection
            };

            await dbContext.Games.AddAsync(game);
        }

        await dbContext.Collections.AddAsync(collection);
        await dbContext.SaveChangesAsync();
    }

    public async Task<CollectionDetailsViewModel?> GetCollectionDetailsByIdAsync(int id)
    {
        var data = await dbContext.Collections
            .Where(c => c.Id == id)
            .Select(c => new
            {
                c.Id,
                c.Title,
                c.User.UserName,
                c.TotalStars,
                Games = c.Games.Select(g => new
                {
                    g.Title,
                    g.Description,
                    g.ImageUrl,
                    GenreName = g.Genre.Name
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (data == null) return null;

        return new CollectionDetailsViewModel
        {
            Id = data.Id,
            Title = data.Title,
            Publisher = data.UserName?.Split('@')[0] ?? "Unknown",
            TotalStars = data.TotalStars,
            Games = data.Games.Select(g => new GameDetailsViewModel
            {
                Title = g.Title,
                Description = g.Description,
                ImageUrl = g.ImageUrl,
                Genre = g.GenreName
            }).ToList()
        };
    }
    public async Task<bool> UserHasCollectionAsync(string userId)
    {

        return await dbContext.Collections.AnyAsync(c => c.UserId == userId);
    }

    public async Task<CollectionFormViewModel?> GetCollectionForEditAsync(int id, string userId)
    {
        var collection = await dbContext.Collections
            .Include(c => c.Games)
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

        if (collection == null) return null;

        var genres = await dbContext.Genres
            .Select(g => new GenreSelectViewModel { Id = g.Id, Name = g.Name })
            .ToListAsync();

        return new CollectionFormViewModel
        {
            Title = collection.Title,
            Games = collection.Games.Select(g => new GameFormViewModel
            {
                Title = g.Title,
                ImageUrl = g.ImageUrl,
                Description = g.Description,
                GenreId = g.GenreId,
                Genres = genres
            }).ToList()
        };
    }

    public async Task EditCollectionAsync(CollectionFormViewModel model, int id)
    {
        var collection = await dbContext.Collections
                .Include(c => c.Games)
                .FirstOrDefaultAsync(c => c.Id == id);

        if (collection != null)
        {
            collection.Title = model.Title;

            for (int i = 0; i < collection.Games.Count; i++)
            {
                var gameEntity = collection.Games.ElementAt(i);
                var gameModel = model.Games[i];

                gameEntity.Title = gameModel.Title;
                gameEntity.Description = gameModel.Description;
                gameEntity.ImageUrl = gameModel.ImageUrl;
                gameEntity.GenreId = gameModel.GenreId;
            }

            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<CollectionDetailsViewModel?> GetCollectionForDeleteAsync(int id, string userId)
    {
        return await GetCollectionDetailsByIdAsync(id);
    }

    public async Task DeleteCollectionAsync(int id, string userId)
    {
        var collection = await dbContext.Collections
            .Include(c => c.Games)
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

        if (collection != null)
        {
            dbContext.Games.RemoveRange(collection.Games);
            dbContext.Collections.Remove(collection);

            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> StarCollectionAsync(int collectionId, string userId)
    {
        var collection = await dbContext.Collections
            .FirstOrDefaultAsync(c => c.Id == collectionId);

        if (collection == null || collection.UserId == userId)
        {
            return false;
        }

        bool alreadyStarred = await dbContext.Likes
            .AnyAsync(l => l.UserId == userId && l.CollectionId == collectionId);

        if (alreadyStarred)
        {
            return false;
        }

        var newLike = new Like
        {
            UserId = userId,
            CollectionId = collectionId
        };

        await dbContext.Likes.AddAsync(newLike);

        collection.TotalStars++;

        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> IsStarredByUserAsync(int collectionId, string userId)
    {
        return await dbContext.Likes
            .AnyAsync(l => l.UserId == userId && l.CollectionId == collectionId);
    }

    public async Task<bool> RemoveStarAsync(int collectionId, string userId)
    {
        var like = await dbContext.Likes
            .FirstOrDefaultAsync(l => l.UserId == userId && l.CollectionId == collectionId);

        if (like == null) return false;

        var collection = await dbContext.Collections.FindAsync(collectionId);
        if (collection != null && collection.TotalStars > 0)
        {
            collection.TotalStars--;
        }

        dbContext.Likes.Remove(like);
        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<CollectionAllViewModel>> GetStarredCollectionsAsync(string userId)
    {
        var starredData = await dbContext.Likes
            .Where(l => l.UserId == userId)
            .Select(l => l.Collection)
            .Select(c => new
            {
                c.Id,
                c.Title,
                PublisherEmail = c.User.UserName,
                c.TotalStars,
                GameImages = c.Games.Select(g => g.ImageUrl).ToList()
            })
            .ToListAsync();

        return starredData.Select(c => new CollectionAllViewModel
        {
            Id = c.Id,
            Title = c.Title,
            Publisher = c.PublisherEmail?.Split('@')[0] ?? "Unknown",
            TotalStars = c.TotalStars,
            GameImages = c.GameImages
        }).ToList();
    }

    public async Task<int> GetUserTotalStarsAsync(string userId)
    {
        return await dbContext.Collections
            .Where(c => c.UserId == userId)
            .Select(c => c.TotalStars)
            .FirstOrDefaultAsync();
    }
}