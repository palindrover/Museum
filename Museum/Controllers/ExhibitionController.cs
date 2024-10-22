using Microsoft.AspNetCore.Mvc;
using Museum.Contexts;
using Microsoft.AspNetCore.Authorization;
using Museum.Models;
using System.Reflection.Metadata.Ecma335;

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

            return View(_context.GetExhibitionById(id));
        }

        [Authorize(Roles = "True")]
        [HttpGet]
        public IActionResult Add()
        {
            var _exhibitsContext = HttpContext.RequestServices.GetService(typeof(ExhibitContext)) as ExhibitContext;
            var _imagesContext = HttpContext.RequestServices.GetService(typeof(FileContext)) as FileContext;

            var _addContext = HttpContext.RequestServices.GetService(typeof(AddExhibitionContext)) as AddExhibitionContext;

            return View(_addContext.GetData(_exhibitsContext.GetAllExhibits(), _imagesContext.GetData()));
        }

        [Authorize(Roles = "True")]
        [HttpPost]
        public IActionResult Add(string exhibitiontitle, string exhibitiondescription, IEnumerable<string> exhibitionimage, IEnumerable<int> exhibitsarray, IEnumerable<string> exhibitsleadup)
        {
            string img = GetImages(exhibitionimage), ex = GetExhibits(exhibitsarray), lu = GetLeadups(exhibitsleadup);

            var _addContext = HttpContext.RequestServices.GetService(typeof(ExhibitionContext)) as ExhibitionContext;
            var _exhibitContext = HttpContext.RequestServices.GetService(typeof(ExhibitContext)) as ExhibitContext;

            int id = _addContext.Add(exhibitiontitle, exhibitiondescription, img, ex, lu);
            _exhibitContext.SetExhibition(string.Join(",", exhibitsarray), id);            

            return RedirectToAction("Index");
        }

        private static string GetImages(IEnumerable<string> images)
        {
            if (images.Count() == 0) return string.Empty;

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

        private string GetExhibits(IEnumerable<int> exhibits)
        {
            string s = string.Empty;

            foreach (int i in exhibits) s += Convert.ToString(i) + ",";

            s.Remove(s.Length - 1, 1);

            return s;
        }

        private string GetLeadups(IEnumerable<string> leadups)
        {
            string s = string.Empty;

            foreach (string i in leadups) s += i + "#";

            s.Remove(s.Length - 1, 1);

            return s;
        }

        [Authorize(Roles = "True")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var _del = HttpContext.RequestServices.GetService(typeof(ExhibitionContext)) as ExhibitionContext;
            var _exhibitsContext = HttpContext.RequestServices.GetService(typeof(ExhibitContext)) as ExhibitContext;

            _del.Delete(id);
            _exhibitsContext.DeleteExhibition(id);

            return RedirectToAction("Index");
        }
    }
}
