using TheBigThree.Web.ViewModels;

namespace TheBigThree.Services.Core.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<UserManagementViewModel>> GetAllUsersAsync();
        Task DeleteUserAsync(string userId);
    }
}
