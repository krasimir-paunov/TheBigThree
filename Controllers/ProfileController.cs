using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheBigThree.Contracts;

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

            ViewBag.Username = User.Identity?.Name?.Split('@')[0] ?? "Gamer";
            ViewBag.TotalStars = totalStarsEarned;
            ViewBag.Rank = rank;

            return View(starredCollections);
        }
    }
}
