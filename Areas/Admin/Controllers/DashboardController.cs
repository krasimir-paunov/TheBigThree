using Microsoft.AspNetCore.Mvc;
using TheBigThree.Services.Core.Interfaces;
using TheBigThree.Web.ViewModels;

namespace TheBigThree.Areas.Admin.Controllers
{
    public class DashboardController : AdminController
    {
        private readonly IAdminService adminService;

        public DashboardController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            AdminDashboardViewModel dashboard = await adminService
                .GetDashboardDataAsync();

            return View(dashboard);
        }
    }
}