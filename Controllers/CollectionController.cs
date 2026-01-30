using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheBigThree.Contracts;

namespace TheBigThree.Controllers
{
    public class CollectionController : Controller
    {
        private readonly ICollectionService collectionService;

        public CollectionController(ICollectionService collectionService)
        {
            this.collectionService = collectionService;
        }

        public async Task<IActionResult> All()
        {
            var model = await collectionService.GetAllCollectionsAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var model = await collectionService.GetMineCollectionsAsync(userId);

            return View(model);
        }
    }
}
