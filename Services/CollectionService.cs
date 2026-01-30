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
        return await dbContext.Collections
            .AsNoTracking()
            .Select(c => new CollectionAllViewModel
            {
                Id = c.Id,
                Title = c.Title,
                Publisher = c.User.UserName!,
                TotalStars = c.TotalStars,
                GameImages = c.Games.Select(g => g.ImageUrl).ToList()
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<CollectionAllViewModel>> GetMineCollectionsAsync(string userId)
    {
        return await dbContext.Collections
            .AsNoTracking()
            .Where(c => c.UserId == userId)
            .Select(c => new CollectionAllViewModel
            {
                Id = c.Id,
                Title = c.Title,
                Publisher = c.User.UserName!,
                TotalStars = c.TotalStars,
                GameImages = c.Games.Select(g => g.ImageUrl).ToList()
            })
            .ToListAsync();
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
        return await dbContext.Collections
            .Where(c => c.Id == id)
            .Select(c => new CollectionDetailsViewModel
            {
                Id = c.Id,
                Title = c.Title,
                Publisher = c.User.UserName!,
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
    }
}