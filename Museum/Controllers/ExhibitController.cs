using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using Museum.Contexts;

namespace Museum.Controllers
{
    public class ExhibitController : Controller
    {
        private ExhibitContext _context;

        public IActionResult Index()
        {
            GetHttpContext();

           return View(_context.GetAllExhibits().OrderBy(el => el.CategoryId).ToList());
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

        public IActionResult Hall(int id)
        {
            GetHttpContext();

            return View("Index", _context.GetByHall(id));
        }

        [Authorize(Roles = "True")]
        [HttpGet]
        public IActionResult AddExhibit()
        {
            var _exhibitCategoryContext = HttpContext.RequestServices.GetService(typeof(CategoryContext)) as CategoryContext;
            var _exhibitHallContext = HttpContext.RequestServices.GetService(typeof(HallContext)) as HallContext;
            var _exhibitImagesContext = HttpContext.RequestServices.GetService(typeof(FileContext)) as FileContext;
            var _result = HttpContext.RequestServices.GetService(typeof(AddExhibitContext)) as AddExhibitContext;

            return View(_result.GetData(_exhibitHallContext.GetAllHalls(), _exhibitCategoryContext.GetCategories(), _exhibitImagesContext.GetData()));
        }

        [Authorize(Roles = "True")]
        [HttpPost]
        public IActionResult AddExhibit(string name, int catid, int hallid, string description, string invnum, IEnumerable<string> images)
        {
            invnum ??= string.Empty;
            description ??= string.Empty;
            name ??= string.Empty;

            string imagesresult = GetImages(images);
            var _AddExContext = HttpContext.RequestServices.GetService(typeof(ExhibitContext)) as ExhibitContext;
            _AddExContext.AddExhibit(name, catid, hallid, description, invnum, imagesresult);
            return RedirectToAction("Index");
        }

        private static string GetImages(IEnumerable<string> images) 
        {
            if(images.Count() == 0) return string.Empty;

            var tempPath = images.First().Split('\\');
            var path = string.Join("/", tempPath);
            var result = path;

            for (int i = 1; i < images.Count(); i++) 
            {
                result += "#";
                tempPath = images.ElementAt(i).Split('\\');
                path = string.Join("/", tempPath);
                result += path;
            }

            return result;
        }
    }
}
