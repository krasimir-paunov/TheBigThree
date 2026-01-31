using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var totalStarsEarned = await collectionService.GetUserTotalStarsAsync(userId);
            var starredCollections = await collectionService.GetStarredCollectionsAsync(userId);

            ViewBag.Username = User.Identity?.Name?.Split('@')[0] ?? "Gamer";
            ViewBag.TotalStars = totalStarsEarned;

            return View(starredCollections);
        }
    }
}
