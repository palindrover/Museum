using Microsoft.AspNetCore.Mvc;
using Museum.Contexts;
using Org.BouncyCastle.Security;

namespace Museum.Controllers
{
    public class HallController : Controller
    {
        private HallContext _context;

        public IActionResult Index()
        {
            GetHttpContext();

            return View(_context.GetAllHalls().OrderBy(el => el.HallAddress));
        }
        private void GetHttpContext()
        {
            _context ??= HttpContext.RequestServices.GetService(typeof(HallContext)) as HallContext;
        }
    }
}
