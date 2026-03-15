using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheBigThree.Data.Models;
using TheBigThree.GCommon;
using TheBigThree.Services.Core.Interfaces;
using TheBigThree.Services.Core.Repositories;
using TheBigThree.Web.ViewModels;

namespace TheBigThree.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IRepository<Collection> collectionRepository;
        private readonly IRepository<Comment> commentRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public ProfileService(
            IRepository<Collection> collectionRepository,
            IRepository<Comment> commentRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.collectionRepository = collectionRepository;
            this.commentRepository = commentRepository;
            this.userManager = userManager;
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

        public async Task<PublicProfileViewModel?> GetPublicProfileAsync(string username)
        {
            ApplicationUser? user = await userManager.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null) return null;

            int totalStars = await collectionRepository.All()
                .Where(c => c.UserId == user.Id)
                .SumAsync(c => c.TotalStars);

            int totalComments = await commentRepository.All()
                .CountAsync(c => c.UserId == user.Id);

            var collection = await collectionRepository.All()
                .AsNoTracking()
                .Where(c => c.UserId == user.Id)
                .Select(c => new
                {
                    c.Id,
                    c.Title,
                    GameImages = c.Games.Select(g => g.ImageUrl).ToList()
                })
                .FirstOrDefaultAsync();

            List<PublicProfileCommentViewModel> recentComments = await commentRepository.All()
                .AsNoTracking()
                .Where(c => c.UserId == user.Id)
                .OrderByDescending(c => c.CreatedOn)
                .Take(5)
                .Select(c => new PublicProfileCommentViewModel
                {
                    Content = c.Content,
                    CollectionTitle = c.Collection.Title,
                    CollectionId = c.CollectionId,
                    CreatedOn = c.CreatedOn
                })
                .ToListAsync();

            return new PublicProfileViewModel
            {
                Username = user.UserName ?? username,
                AvatarUrl = user.AvatarUrl,
                Rank = RankHelper.GetRank(totalStars),
                TotalStars = totalStars,
                TotalComments = totalComments,
                MemberSince = user.CreatedOn,
                CollectionTitle = collection?.Title,
                CollectionId = collection?.Id,
                CollectionGameImages = collection?.GameImages ?? new List<string>(),
                RecentComments = recentComments
            };
        }
    }
}