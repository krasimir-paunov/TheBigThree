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

            TempData["Success"] = "User deleted.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Promote(string id)
        {
            await adminService.PromoteToAdminAsync(id);

            TempData["Success"] = "User promoted to Administrator.";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Demote(string id)
        {
            await adminService.DemoteFromAdminAsync(id);

            TempData["Success"] = "User demoted to User.";

            return RedirectToAction(nameof(Index));
        }
    }
}