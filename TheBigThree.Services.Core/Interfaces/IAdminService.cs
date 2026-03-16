using TheBigThree.Web.ViewModels;

namespace TheBigThree.Services.Core.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<UserManagementViewModel>> GetAllUsersAsync();
        Task DeleteUserAsync(string userId);
        Task<IEnumerable<CollectionManagementViewModel>> GetAllCollectionsAsync();
        Task DeleteCollectionAsync(int collectionId);
        Task<AdminDashboardViewModel> GetDashboardDataAsync();
        Task PromoteToAdminAsync(string userId);
        Task DemoteFromAdminAsync(string userId);
    }
}
