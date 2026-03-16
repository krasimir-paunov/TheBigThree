using Microsoft.AspNetCore.Mvc;
using TheBigThree.Services.Core.Interfaces;
using TheBigThree.Web.ViewModels;

namespace TheBigThree.Areas.Admin.Controllers
{
    public class UsersController : AdminController
    {
        private readonly IAdminService adminService;

        public UsersController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<UserManagementViewModel> users = await adminService.GetAllUsersAsync();

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await adminService.DeleteUserAsync(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Promote(string id)
        {
            await adminService.PromoteToAdminAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Demote(string id)
        {
            await adminService.DemoteFromAdminAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}