using TheBigThree.Data.Models;

namespace TheBigThree.Services.Core.Interfaces
{
    public interface ICommentService
    {
        Task<Comment> GetCommentsByCollectionIdAsync(int collectionId);

        Task AddCommentAsync(string content, int collectionId, string userId);

        Task DeleteCommentAsync(int commentId, string userId);

        Task AdminDeleteCommentAsync(int commentId);

    }
}
