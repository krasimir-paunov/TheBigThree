using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheBigThree.Data;
using TheBigThree.Data.Models;
using TheBigThree.Services.Core.Interfaces;
using TheBigThree.Services.Core.Repositories;
using TheBigThree.Web.ViewModels;

namespace TheBigThree.Services
{
    public class AdminService : IAdminService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRepository<Collection> collectionRepository;
        private readonly IRepository<Comment> commentRepository;
        private readonly TheBigThreeDbContext context;

        public AdminService(
            UserManager<ApplicationUser> userManager,
            IRepository<Collection> collectionRepository,
            IRepository<Comment> commentRepository,
            TheBigThreeDbContext context)
        {
            this.userManager = userManager;
            this.collectionRepository = collectionRepository;
            this.commentRepository = commentRepository;
            this.context = context;
        }

        public async Task<IEnumerable<UserManagementViewModel>> GetAllUsersAsync()
        {
            List<ApplicationUser> users = await userManager
                .Users
                .AsNoTracking()
                .ToListAsync();

            List<UserManagementViewModel> result = new List<UserManagementViewModel>();

            foreach (ApplicationUser user in users)
            {
                IList<string> roles = await userManager.GetRolesAsync(user);

                result.Add(new UserManagementViewModel
                {
                    Id = user.Id,
                    Username = user.UserName ?? "Unknown",
                    Email = user.Email ?? "Unknown",
                    Role = roles.FirstOrDefault() ?? "User",
                    HasCollection = await collectionRepository.All()
                        .AnyAsync(c => c.UserId == user.Id),
                    CollectionId = await collectionRepository.All()
                        .Where(c => c.UserId == user.Id)
                        .Select(c => c.Id)
                        .FirstOrDefaultAsync()
                });
            }

            return result;
        }

        public async Task DeleteUserAsync(string userId)
        {
            ApplicationUser? user = await userManager.FindByIdAsync(userId);

            if (user == null) return;

            await userManager.DeleteAsync(user);
        }

        public async Task<IEnumerable<CollectionManagementViewModel>> GetAllCollectionsAsync()
        {
            return await collectionRepository
                .All()
                .AsNoTracking()
                .Select(c => new CollectionManagementViewModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    Owner = c.User.UserName ?? "Unknown",
                    TotalStars = c.TotalStars,
                    TotalComments = c.Comments.Count(),
                    CreatedOn = c.CreatedOn
                })
                .OrderByDescending(c => c.TotalStars)
                .ToListAsync();
        }

        public async Task DeleteCollectionAsync(int collectionId)
        {
            Collection? collection = await collectionRepository
                .All()
                .FirstOrDefaultAsync(c => c.Id == collectionId);

            if (collection == null) return;

            List<Like> likes = await context.Likes
                .Where(l => l.CollectionId == collectionId)
                .ToListAsync();

            context.Likes.RemoveRange(likes);

            await context.SaveChangesAsync();

            await collectionRepository.DeleteAsync(collectionId);

            await collectionRepository.SaveChangesAsync();
        }

        public async Task<AdminDashboardViewModel> GetDashboardDataAsync()
        {
            int totalUsers = await userManager.Users.CountAsync();

            int totalCollections = await collectionRepository.All().CountAsync();

            int totalStars = await collectionRepository.All().SumAsync(c => c.TotalStars);

            int totalComments = await commentRepository.All().CountAsync();

            Collection? topCollection = await collectionRepository
                .All()
                .AsNoTracking()
                .Include(c => c.User)
                .OrderByDescending(c => c.TotalStars)
                .FirstOrDefaultAsync();

            string? mostActiveUser = await commentRepository
                .All()
                .GroupBy(c => c.User.UserName)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefaultAsync();

            int mostActiveUserComments = await commentRepository
                .All()
                .GroupBy(c => c.User.UserName)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Count())
                .FirstOrDefaultAsync();

            List<CollectionManagementViewModel> recentCollections = await collectionRepository
                .All()
                .AsNoTracking()
                .OrderByDescending(c => c.CreatedOn)
                .Take(5)
                .Select(c => new CollectionManagementViewModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    Owner = c.User.UserName ?? "Unknown",
                    TotalStars = c.TotalStars,
                    TotalComments = c.Comments.Count(),
                    CreatedOn = c.CreatedOn
                })
                .ToListAsync();

            return new AdminDashboardViewModel
            {
                TotalUsers = totalUsers,
                TotalCollections = totalCollections,
                TotalStars = totalStars,
                TotalComments = totalComments,
                TopCollectionTitle = topCollection?.Title ?? "N/A",
                TopCollectionOwner = topCollection?.User.UserName ?? "N/A",
                TopCollectionStars = topCollection?.TotalStars ?? 0,
                TopCollectionId = topCollection?.Id ?? 0,
                MostActiveUser = mostActiveUser ?? "N/A",
                MostActiveUserComments = mostActiveUserComments,
                RecentCollections = recentCollections
            };
        }

        public async Task PromoteToAdminAsync(string userId)
        {
            ApplicationUser? user = await userManager.FindByIdAsync(userId);

            if (user == null) return;

            await userManager.AddToRoleAsync(user, "Administrator");
        }

        public async Task DemoteFromAdminAsync(string userId)
        {
            ApplicationUser? user = await userManager.FindByIdAsync(userId);

            if (user == null) return;

            await userManager.RemoveFromRoleAsync(user, "Administrator");
        }
    }
}