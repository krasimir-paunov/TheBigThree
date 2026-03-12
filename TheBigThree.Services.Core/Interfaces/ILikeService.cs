using TheBigThree.Web.ViewModels;

namespace TheBigThree.Services.Core.Interfaces
{
    public interface ILikeService
    {
        Task StarCollectionAsync(int collectionId, string userId);
        Task RemoveStarAsync(int collectionId, string userId);
        Task<bool> IsStarredByUserAsync(int collectionId, string userId);
        Task<IEnumerable<CollectionAllViewModel>> GetStarredCollectionsAsync(string userId);
    }
}
