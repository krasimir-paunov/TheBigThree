using Microsoft.AspNetCore.Mvc;
using TheBigThree.Services.Core.Interfaces;
using TheBigThree.Web.ViewModels;

namespace TheBigThree.Areas.Admin.Controllers
{
    public class CollectionsController : AdminController
    {
        private readonly IAdminService adminService;

        public CollectionsController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<CollectionManagementViewModel> collections = await adminService.GetAllCollectionsAsync();

            return View(collections);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await adminService.DeleteCollectionAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}