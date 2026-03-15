using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheBigThree.Services.Core.Interfaces;

namespace TheBigThree.Controllers
{
    [Authorize]
    public class PublicProfileController : Controller
    {
        private readonly IProfileService profileService;

        public PublicProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return RedirectToAction("All", "Collection");
            }

            var viewModel = await profileService.GetPublicProfileAsync(username);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }
    }
}