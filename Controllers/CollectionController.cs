using Microsoft.AspNetCore.Mvc;

namespace TheBigThree.Controllers
{
    public class CollectionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
