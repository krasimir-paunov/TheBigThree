using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheBigThree.Contracts;
using TheBigThree.ViewModels; 

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
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            string email = User.FindFirstValue(ClaimTypes.Email) ?? "N/A";
            string username = User.Identity?.Name?.Split('@')[0] ?? "Gamer";

            var totalStarsEarned = await collectionService.GetUserTotalStarsAsync(userId);
            var starredCollections = await collectionService.GetStarredCollectionsAsync(userId);

            string rank = totalStarsEarned switch
            {
                >= 100 => "Legendary Collector",
                >= 30 => "Superstar Collector",
                >= 10 => "Popular Collector",
                >= 5 => "Rising Star",
                > 0 => "Novice Collector",
                _ => "Newcomer"
            };

            var viewModel = new ProfileViewModel
            {
                Username = username,
                Email = email,
                Rank = rank,
                TotalStarsEarned = totalStarsEarned,
                FavoriteCollections = starredCollections
            };

            return View(viewModel);
        }
    }
}