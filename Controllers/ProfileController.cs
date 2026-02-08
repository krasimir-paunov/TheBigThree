using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheBigThree.Contracts;
using TheBigThree.Web.ViewModels;

namespace TheBigThree.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ICollectionService collectionService;

        public ProfileController(ICollectionService collectionService)
        {
            this.collectionService = collectionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                string email = User.FindFirstValue(ClaimTypes.Email) ?? "N/A";
                string username = User.Identity?.Name?.Split('@')[0] ?? "Gamer";

                int totalStarsEarned = await collectionService.GetUserTotalStarsAsync(userId);
                IEnumerable<CollectionAllViewModel> starredCollections = await collectionService.GetStarredCollectionsAsync(userId);

                string rank = GetRankName(totalStarsEarned);

                ProfileViewModel viewModel = new ProfileViewModel
                {
                    Username = username,
                    Email = email,
                    Rank = rank,
                    TotalStarsEarned = totalStarsEarned,
                    FavoriteCollections = starredCollections
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                TempData["Error"] = "We encountered a problem loading your profile. Please try again later.";
                return RedirectToAction("Index", "Home");
            }
        }

        private string GetRankName(int stars) => stars switch
        {
            >= 100 => "Legendary Collector",
            >= 30 => "Superstar Collector",
            >= 10 => "Popular Collector",
            >= 5 => "Rising Star",
            >= 1 => "Novice Collector",
            _ => "Newcomer"
        };
    }
}