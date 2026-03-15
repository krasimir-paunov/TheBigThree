using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheBigThree.Services.Core.Interfaces;

namespace TheBigThree.Controllers
{
    [Authorize]
    public class LeaderboardController : Controller
    {
        private readonly IStatsService statsService;

        public LeaderboardController(IStatsService statsService)
        {
            this.statsService = statsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = await statsService.GetLeaderboardAsync();

            return View(viewModel);
        }
    }
}