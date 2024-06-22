using Microsoft.AspNetCore.Mvc;

namespace Museum.Controllers
{
    public class ExhibitionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
