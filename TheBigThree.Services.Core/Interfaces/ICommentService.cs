using TheBigThree.Web.ViewModels;

namespace TheBigThree.Services.Core.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentViewModel>> GetCommentsByCollectionIdAsync(int collectionId);

        Task AddCommentAsync(AddCommentViewModel content, int collectionId, string userId);

        Task DeleteCommentAsync(int commentId, string userId);

        Task AdminDeleteCommentAsync(int commentId);

    }
}
