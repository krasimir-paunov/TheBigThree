using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheBigThree.Contracts;
using TheBigThree.ViewModels;

namespace TheBigThree.Controllers
{
    [Authorize]
    public class CollectionController : Controller
    {
        private readonly ICollectionService collectionService;

        public CollectionController(ICollectionService collectionService)
        {
            this.collectionService = collectionService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var model = await collectionService.GetAllCollectionsAsync();

            if (User.Identity?.IsAuthenticated == true)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                ViewBag.UserHasCollection = await collectionService.UserHasCollectionAsync(userId);
            }
            else
            {
                ViewBag.UserHasCollection = false;
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var model = await collectionService.GetMineCollectionsAsync(userId);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            if (await collectionService.UserHasCollectionAsync(userId))
            {
                return RedirectToAction(nameof(Mine));
            }

            var model = await collectionService.GetNewAddFormModelAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CollectionFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var refreshModel = await collectionService.GetNewAddFormModelAsync();
                model.Games.ForEach(g => g.Genres = refreshModel.Games[0].Genres);

                return View(model);
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            await collectionService.AddCollectionAsync(model, userId);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var model = await collectionService.GetCollectionDetailsByIdAsync(id);

            if (model == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(model);
        }
    }
}
