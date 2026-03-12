using TheBigThree.Web.ViewModels;

namespace TheBigThree.Services.Core.Interfaces
{
    public interface ICollectionService
    {
        Task<CollectionQueryModel> GetAllCollectionsAsync(CollectionQueryModel query);

        Task<IEnumerable<CollectionAllViewModel>> GetMineCollectionsAsync(string userId);

        Task<CollectionFormViewModel> GetNewAddFormModelAsync();

        Task AddCollectionAsync(CollectionFormViewModel model, string userId);

        Task<CollectionDetailsViewModel?> GetCollectionDetailsByIdAsync(int id);

        Task<bool> UserHasCollectionAsync(string userId);

        Task<CollectionFormViewModel?> GetCollectionForEditAsync(int id, string userId);

        Task EditCollectionAsync(CollectionFormViewModel model, int id, string userId);

        Task<CollectionDetailsViewModel?> GetCollectionForDeleteAsync(int id, string userId);

        Task DeleteCollectionAsync(int id, string userId);
    }
}