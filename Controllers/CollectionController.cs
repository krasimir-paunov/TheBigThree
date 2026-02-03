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
        public async Task<IActionResult> All(string sorting)
        {
            IEnumerable<CollectionAllViewModel> hubCollections = await collectionService.GetAllCollectionsAsync(sorting);
            ViewBag.CurrentSort = sorting;

            if (User.Identity?.IsAuthenticated == true)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                ViewBag.UserHasCollection = await collectionService.UserHasCollectionAsync(userId);
            }
            else
            {
                ViewBag.UserHasCollection = false;
            }

            return View(hubCollections);
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            IEnumerable<CollectionAllViewModel> personalCollections = await collectionService.GetMineCollectionsAsync(userId);
            return View(personalCollections);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            CollectionDetailsViewModel? collectionDetails = await collectionService.GetCollectionDetailsByIdAsync(id);
            if (collectionDetails == null) return RedirectToAction(nameof(All));

            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.IsStarred = userId != null && await collectionService.IsStarredByUserAsync(id, userId);

            return View(collectionDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            if (await collectionService.UserHasCollectionAsync(userId))
            {
                return RedirectToAction(nameof(Mine));
            }

            CollectionFormViewModel newCollectionForm = await collectionService.GetNewAddFormModelAsync();
            return View(newCollectionForm);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CollectionFormViewModel collectionInput)
        {
            if (!ModelState.IsValid)
            {
                await RefreshGenreData(collectionInput);
                return View(collectionInput);
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            if (await collectionService.UserHasCollectionAsync(userId))
            {
                TempData["Error"] = "You already have a 'Big Three' collection!";
                return RedirectToAction(nameof(Mine));
            }

            try
            {
                await collectionService.AddCollectionAsync(collectionInput, userId);
                return RedirectToAction(nameof(Mine));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An unexpected error occurred while saving. Please try again.");
                await RefreshGenreData(collectionInput);
                return View(collectionInput);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            CollectionFormViewModel? existingCollection = await collectionService.GetCollectionForEditAsync(id, userId);

            if (existingCollection == null) return RedirectToAction(nameof(Mine));

            return View(existingCollection);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CollectionFormViewModel updatedInput)
        {
            if (!ModelState.IsValid)
            {
                await RefreshGenreData(updatedInput);
                return View(updatedInput);
            }

            try
            {
                await collectionService.EditCollectionAsync(updatedInput, id);
                return RedirectToAction(nameof(Mine));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to save changes. The database might be temporarily unavailable.");
                await RefreshGenreData(updatedInput);
                return View(updatedInput);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            CollectionDetailsViewModel? collectionToDelete = await collectionService.GetCollectionForDeleteAsync(id, userId);

            if (collectionToDelete == null) return RedirectToAction(nameof(Mine));

            return View(collectionToDelete);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            try
            {
                await collectionService.DeleteCollectionAsync(id, userId);
                return RedirectToAction(nameof(Mine));
            }
            catch (Exception)
            {
                TempData["Error"] = "An error occurred while trying to delete your collection.";
                return RedirectToAction(nameof(Mine));
            }
        }


        [HttpPost]
        public async Task<IActionResult> Star(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            try
            {
                await collectionService.StarCollectionAsync(id, userId);
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
            }
            catch (Exception)
            {
                TempData["Error"] = "Unexpected error during the Star operation.";
            }

            return RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveStar(int id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            try
            {
                await collectionService.RemoveStarAsync(id, userId);
            }
            catch (Exception)
            {
                TempData["Error"] = "An error occurred while removing your star.";
            }

            return RedirectToAction(nameof(Details), new { id = id });
        }

        private async Task RefreshGenreData(CollectionFormViewModel formToRefresh)
        {
            CollectionFormViewModel freshData = await collectionService.GetNewAddFormModelAsync();
            foreach (GameFormViewModel game in formToRefresh.Games)
            {
                game.Genres = freshData.Games[0].Genres;
            }
        }
    }
}