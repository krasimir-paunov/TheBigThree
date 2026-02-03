using TheBigThree.ViewModels;

namespace TheBigThree.Contracts
{
    public interface ICollectionService
    {
        Task<IEnumerable<CollectionAllViewModel>> GetAllCollectionsAsync(string? sorting = null);

        Task<IEnumerable<CollectionAllViewModel>> GetMineCollectionsAsync(string userId);

        Task<CollectionFormViewModel> GetNewAddFormModelAsync();

        Task AddCollectionAsync(CollectionFormViewModel model, string userId);

        Task<CollectionDetailsViewModel?> GetCollectionDetailsByIdAsync(int id);

        Task<bool> UserHasCollectionAsync(string userId);

        Task<CollectionFormViewModel?> GetCollectionForEditAsync(int id, string userId);

        Task EditCollectionAsync(CollectionFormViewModel model, int id);

        Task<CollectionDetailsViewModel?> GetCollectionForDeleteAsync(int id, string userId);

        Task DeleteCollectionAsync(int id, string userId);

        Task StarCollectionAsync(int collectionId, string userId);

        Task RemoveStarAsync(int collectionId, string userId);

        Task<bool> IsStarredByUserAsync(int collectionId, string userId);

        Task<IEnumerable<CollectionAllViewModel>> GetStarredCollectionsAsync(string userId);

        Task<int> GetUserTotalStarsAsync(string userId);
    }
}