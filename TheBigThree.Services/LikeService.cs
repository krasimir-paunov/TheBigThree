using Microsoft.EntityFrameworkCore;
using TheBigThree.Data;
using TheBigThree.Data.Models;
using TheBigThree.Services.Core.Interfaces;
using TheBigThree.Services.Core.Repositories;
using TheBigThree.Web.ViewModels;

namespace TheBigThree.Services
{
    public class LikeService : ILikeService
    {
        private readonly IRepository<Like> likeRepository;
        private readonly IRepository<Collection> collectionRepository;
        private readonly TheBigThreeDbContext dbContext;

        public LikeService(IRepository<Like> likeRepository, IRepository<Collection> collectionRepository, TheBigThreeDbContext dbContext)
        {
            this.likeRepository = likeRepository;
            this.collectionRepository = collectionRepository;
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

        public async Task StarCollectionAsync(int collectionId, string userId)
        {
            Collection? targetCollection = await collectionRepository.All()
                .FirstOrDefaultAsync(c => c.Id == collectionId);

            if (targetCollection == null) throw new ArgumentException("Collection does not exist.");

            if (targetCollection.UserId == userId) throw new InvalidOperationException("You cannot star your own collection.");

            bool isPreviouslyStarred = await likeRepository.All()
                .AnyAsync(l => l.UserId == userId && l.CollectionId == collectionId);

            if (isPreviouslyStarred) throw new InvalidOperationException("You have already starred this collection.");

            Like starEntry = new Like { UserId = userId, CollectionId = collectionId };

            await likeRepository.AddAsync(starEntry);

            targetCollection.TotalStars++;

            await likeRepository.SaveChangesAsync();
        }

        public async Task RemoveStarAsync(int collectionId, string userId)
        {
            Like? existingStar = await likeRepository.All()
                .FirstOrDefaultAsync(l => l.UserId == userId && l.CollectionId == collectionId);

            if (existingStar == null) return;

            Collection? targetCollection = await collectionRepository.GetByIdAsync(collectionId);

            if (targetCollection != null && targetCollection.TotalStars > 0) targetCollection.TotalStars--;

            dbContext.Set<Like>().Remove(existingStar);

            await likeRepository.SaveChangesAsync();
        }

        public async Task<bool> IsStarredByUserAsync(int collectionId, string userId)
        {
            bool isStarred = await likeRepository.All()
                .AnyAsync(l => l.UserId == userId && l.CollectionId == collectionId);

            return isStarred;
        }

        public async Task<IEnumerable<CollectionAllViewModel>> GetStarredCollectionsAsync(string userId)
        {
            List<CollectionAllViewModel> favoriteCollections = await likeRepository.All()
                .Where(l => l.UserId == userId)
                .Select(l => l.Collection)
                .OrderByDescending(c => c.TotalStars)
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

            foreach (CollectionAllViewModel collection in favoriteCollections)
            {
                collection.Publisher = collection.Publisher.Split('@')[0];

                collection.PublisherRank = CalculateRank(collection.TotalStars);
            }

            return favoriteCollections;
        }
    }
}