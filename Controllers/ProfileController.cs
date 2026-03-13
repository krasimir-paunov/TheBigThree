using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheBigThree.Data.Models;
using TheBigThree.Services.Core.Interfaces;
using TheBigThree.Web.ViewModels;

namespace TheBigThree.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ILikeService likeService;
        private readonly IProfileService profileService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProfileController(ILikeService likeService, IProfileService profileService, UserManager<ApplicationUser> userManager)
        {
            this.likeService = likeService;
            this.profileService = profileService;
            this.userManager = userManager;
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

                string username = User.Identity?.Name ?? "Gamer";

                ApplicationUser? appUser = await userManager.FindByIdAsync(userId);

                int totalStarsEarned = await profileService.GetUserTotalStarsAsync(userId);

                IEnumerable<CollectionAllViewModel> starredCollections = await likeService.GetStarredCollectionsAsync(userId);

                var ownCollection = await profileService.GetOwnCollectionPreviewAsync(userId);

                string rank = GetRankName(totalStarsEarned);

                ProfileViewModel viewModel = new ProfileViewModel
                {
                    Username = username,
                    Email = email,
                    Rank = rank,
                    TotalStarsEarned = totalStarsEarned,
                    FavoriteCollections = starredCollections,
                    AvatarUrl = appUser?.AvatarUrl,
                    OwnCollectionTitle = ownCollection.Title,
                    OwnCollectionId = ownCollection.Id,
                    OwnCollectionGameImages = ownCollection.GameImages
                };

                return View(viewModel);
            }
            catch (Exception)
            {
                TempData["Error"] = "We encountered a problem loading your profile. Please try again later.";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAvatar(string? avatarUrl)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return RedirectToAction(nameof(Index));

            ApplicationUser? appUser = await userManager.FindByIdAsync(userId);
            if (appUser == null) return RedirectToAction(nameof(Index));

            appUser.AvatarUrl = string.IsNullOrWhiteSpace(avatarUrl) ? null : avatarUrl;
            await userManager.UpdateAsync(appUser);

            return RedirectToAction(nameof(Index));
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