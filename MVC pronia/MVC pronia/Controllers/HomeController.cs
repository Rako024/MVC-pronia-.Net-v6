using Microsoft.AspNetCore.Mvc;

namespace MVC_pronia.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
