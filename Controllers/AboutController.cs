using Microsoft.AspNetCore.Mvc;

namespace OdeToFood.Controllers
{
    public class AboutController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}