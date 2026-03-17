using Microsoft.EntityFrameworkCore;
using TheBigThree.Data;
using TheBigThree.Data.Models;
using TheBigThree.Services.Core.Interfaces;
using TheBigThree.Services.Core.Repositories;
using TheBigThree.Web.ViewModels;

namespace TheBigThree.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> commentRepository;

        private readonly TheBigThreeDbContext dbContext; 

        public CommentService(IRepository<Comment> commentRepository, TheBigThreeDbContext dbContext)
        {
            this.commentRepository = commentRepository;
            this.dbContext = dbContext;
        }

        public async Task AddCommentAsync(AddCommentViewModel content, int collectionId, string userId)
        {
            Comment newComment = new Comment()
            {
                Content = content.Content,
                CreatedOn = DateTime.UtcNow,
                UserId = userId,
                CollectionId = collectionId
            };

            await commentRepository.AddAsync(newComment);

            await commentRepository.SaveChangesAsync();
        }

        public async Task AdminDeleteCommentAsync(int commentId)
        {
            var commentToDelete = await commentRepository.GetByIdAsync(commentId);

            if (commentToDelete == null)
            {
                throw new ArgumentException($"Comment with ID {commentId} does not exist.");

            }

            await commentRepository.DeleteAsync(commentId);
            await commentRepository.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(int commentId, string userId, bool isAdmin = false)
        {
            var commentToDelete = await commentRepository.GetByIdAsync(commentId);

            if (commentToDelete == null)
            {
                throw new ArgumentException($"Comment with ID {commentId} does not exist.");
            }
            if (commentToDelete.UserId != userId && !isAdmin)
            {
                throw new UnauthorizedAccessException("You do not have permission to delete this comment.");
            }

            await commentRepository.DeleteAsync(commentId);

            await commentRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CommentViewModel>> GetCommentsByCollectionIdAsync(int collectionId)
        {
            return await commentRepository.All()
                .Include(c => c.User)
                .Where(c => c.CollectionId == collectionId)
                .Select(c => new CommentViewModel()
                {
                    Id = c.Id,
                    Content = c.Content,
                    CreatedOn = c.CreatedOn,
                    UserName = c.User.UserName ?? "Unknown",
                    UserId = c.UserId,
                    AvatarUrl = dbContext.Users.OfType<ApplicationUser>()
                        .Where(u => u.Id == c.UserId)
                        .Select(u => u.AvatarUrl)
                        .FirstOrDefault()
                })
                .OrderByDescending(c => c.CreatedOn)
                .ToListAsync();
        }
    }
}
