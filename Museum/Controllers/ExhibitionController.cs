using Microsoft.AspNetCore.Mvc;
using Museum.Contexts;
using Museum.Models;

namespace Museum.Controllers
{
    public class ExhibitionController : Controller
    {
        private ExhibitionContext _context;

        public IActionResult Index()
        {
            GetHttpContext();

            return View(_context.GetAllExhibitions());
        }

        private void GetHttpContext()
        {
            _context ??= HttpContext.RequestServices.GetService(typeof(ExhibitionContext)) as ExhibitionContext;
        }

        public IActionResult Details(int id)
        {
            GetHttpContext();

            return View(_context.GetById(id));
        }
    }
}
