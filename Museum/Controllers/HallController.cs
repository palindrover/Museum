using Microsoft.AspNetCore.Mvc;

namespace Museum.Controllers
{
    public class HallController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
