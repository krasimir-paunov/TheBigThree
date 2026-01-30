using TheBigThree.ViewModels;

namespace TheBigThree.Contracts
{
    public interface ICollectionService
    {
        Task<IEnumerable<CollectionAllViewModel>> GetAllCollectionsAsync();
        Task<IEnumerable<CollectionAllViewModel>> GetMineCollectionsAsync(string userId);

        Task<CollectionFormViewModel> GetNewAddFormModelAsync();

        Task AddCollectionAsync(CollectionFormViewModel model, string userId);

        Task<CollectionDetailsViewModel?> GetCollectionDetailsByIdAsync(int id);
        Task<bool> UserHasCollectionAsync(string userId);
    }
}
