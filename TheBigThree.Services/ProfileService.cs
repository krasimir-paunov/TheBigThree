using Microsoft.EntityFrameworkCore;
using TheBigThree.Data.Models;
using TheBigThree.Services.Core.Interfaces;
using TheBigThree.Services.Core.Repositories;

namespace TheBigThree.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IRepository<Collection> collectionRepository;

        public ProfileService(IRepository<Collection> collectionRepository)
        {
            this.collectionRepository = collectionRepository;
        }

        public async Task<int> GetUserTotalStarsAsync(string userId)
        {
            int totalStarsCount = await collectionRepository.All()
                .Where(c => c.UserId == userId)
                .Select(c => c.TotalStars)
                .FirstOrDefaultAsync();

            return totalStarsCount;
        }

        public async Task<(string? Title, int? Id, List<string> GameImages)> GetOwnCollectionPreviewAsync(string userId)
        {
            var collection = await collectionRepository.All()
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .Select(c => new
                {
                    c.Id,
                    c.Title,
                    GameImages = c.Games.Select(g => g.ImageUrl).ToList()
                })
                .FirstOrDefaultAsync();

            if (collection == null)
                return (null, null, new List<string>());

            return (collection.Title, collection.Id, collection.GameImages);
        }
    }

}