using Microsoft.AspNetCore.Mvc;

namespace WebTechInMemory.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
