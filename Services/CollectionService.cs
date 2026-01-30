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
}