using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Museum.Contexts;

namespace Museum.Controllers
{
    public class ExhibitController : Controller
    {
        private ExhibitContext _context;

        public IActionResult Index()
        {
            GetHttpContext();

           return View(_context.GetAllExhibits());
        }

        private void GetHttpContext()
        {
            _context ??= HttpContext.RequestServices.GetService(typeof(ExhibitContext)) as ExhibitContext;
        }

        public IActionResult Category(int categId)
        {
            GetHttpContext();

            return View(_context.GetByCategory(categId));
        }

        public IActionResult Details(int id)
        {
            GetHttpContext();

            return View(_context.GetExhibitById(id));
        }

    }
}
