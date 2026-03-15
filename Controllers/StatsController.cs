using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheBigThree.Services.Core.Interfaces;

namespace TheBigThree.Controllers
{
    [Authorize]
    public class StatsController : Controller
    {
        private readonly IStatsService statsService;

        public StatsController(IStatsService statsService)
        {
            this.statsService = statsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var viewModel = await statsService.GetStatsAsync();

            return View(viewModel);
        }
    }
}