using TheBigThree.ViewModels;

namespace TheBigThree.Contracts
{
    public interface ICollectionService
    {
        Task<IEnumerable<CollectionAllViewModel>> GetAllCollectionsAsync();
    }
}
