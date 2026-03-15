using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheBigThree.Data.Models;
using TheBigThree.GCommon;
using TheBigThree.Services.Core.Interfaces;
using TheBigThree.Services.Core.Repositories;
using TheBigThree.Web.ViewModels;

namespace TheBigThree.Services
{
    public class StatsService : IStatsService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRepository<Collection> collectionRepository;
        private readonly IRepository<Comment> commentRepository;
        private readonly IRepository<Game> gameRepository;

        public StatsService(
            UserManager<ApplicationUser> userManager,
            IRepository<Collection> collectionRepository,
            IRepository<Comment> commentRepository,
            IRepository<Game> gameRepository)
        {
            this.userManager = userManager;
            this.collectionRepository = collectionRepository;
            this.commentRepository = commentRepository;
            this.gameRepository = gameRepository;
        }

        public async Task<StatsViewModel> GetStatsAsync()
        {
            int totalUsers = await userManager.Users.CountAsync();

            int totalCollections = await collectionRepository.All().CountAsync();

            int totalStars = await collectionRepository.All().SumAsync(c => c.TotalStars);

            int totalComments = await commentRepository.All().CountAsync();

            List<TopCollectionViewModel> topThree = await collectionRepository
                .All()
                .AsNoTracking()
                .Include(c => c.User)
                .OrderByDescending(c => c.TotalStars)
                .Take(3)
                .Select(c => new TopCollectionViewModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    Owner = c.User.UserName ?? "Unknown",
                    TotalStars = c.TotalStars,
                    AvatarUrl = c.User.AvatarUrl
                })
                .ToListAsync();

            var mostCommented = await collectionRepository
                .All()
                .AsNoTracking()
                .OrderByDescending(c => c.Comments.Count())
                .Select(c => new
                {
                    c.Id,
                    c.Title,
                    CommentCount = c.Comments.Count()
                })
                .FirstOrDefaultAsync();

            string mostActiveCommenter = await commentRepository
                .All()
                .GroupBy(c => c.User.UserName)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefaultAsync() ?? "N/A";

            int mostActiveCommenterCount = await commentRepository
                .All()
                .GroupBy(c => c.User.UserName)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Count())
                .FirstOrDefaultAsync();

            var newest = await collectionRepository
                .All()
                .AsNoTracking()
                .Include(c => c.User)
                .OrderByDescending(c => c.CreatedOn)
                .Select(c => new
                {
                    c.Id,
                    c.Title,
                    Owner = c.User.UserName ?? "Unknown"
                })
                .FirstOrDefaultAsync();

            List<GenreStatViewModel> topGenres = await gameRepository
                .All()
                .AsNoTracking()
                .GroupBy(g => g.Genre.Name)
                .Select(g => new GenreStatViewModel
                {
                    Genre = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(g => g.Count)
                .Take(5)
                .ToListAsync();

            foreach (TopCollectionViewModel collection in topThree)
            {
                collection.PublisherRank = RankHelper.GetRank(collection.TotalStars);
            }

            return new StatsViewModel
            {
                TotalUsers = totalUsers,
                TotalCollections = totalCollections,
                TotalStars = totalStars,
                TotalComments = totalComments,
                TopThreeCollections = topThree,
                TopGenres = topGenres,
                MostCommentedCollectionTitle = mostCommented?.Title ?? "N/A",
                MostCommentedCollectionId = mostCommented?.Id ?? 0,
                MostCommentedCollectionComments = mostCommented?.CommentCount ?? 0,
                MostActiveCommenter = mostActiveCommenter,
                MostActiveCommenterCount = mostActiveCommenterCount,
                NewestCollectionTitle = newest?.Title ?? "N/A",
                NewestCollectionId = newest?.Id ?? 0,
                NewestCollectionOwner = newest?.Owner ?? "N/A"
            };
        }
    }
}