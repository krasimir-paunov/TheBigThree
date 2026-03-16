using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheBigThree.Services.Core.Interfaces;

namespace TheBigThree.Controllers
{
    [Authorize]
    public class RawgController : Controller
    {
        private readonly IRawgService rawgService;

        public RawgController(IRawgService rawgService)
        {
            this.rawgService = rawgService;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return Json(new List<object>());
            }
                

            var results = await rawgService.SearchGamesAsync(query);

            return Json(results);
        }

        [HttpGet]
        public async Task<IActionResult> GameDetails(string gameName)
        {
            if (!rawgService.IsConfigured())
            {
                return Json(new { error = "API key not configured" });
            }
               

            if (string.IsNullOrWhiteSpace(gameName))
                return BadRequest();

            var details = await rawgService.GetGameDetailsAsync(gameName);

            if (details == null)
            {
                return Json(new { error = "Game not found" });
            }

            return Json(details);
        }
    }
}