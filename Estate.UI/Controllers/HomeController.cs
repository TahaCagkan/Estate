using Microsoft.AspNetCore.Mvc;

namespace Estate.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
