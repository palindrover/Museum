using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Museum.Contexts;
using Museum.Models;
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

        [Authorize(Roles = "True")]
        [HttpGet]
        public IActionResult AddHall()
        {
            var _imageContext = HttpContext.RequestServices.GetService(typeof(FileContext)) as FileContext;

            return View(_imageContext.GetData());
        }

        [Authorize(Roles = "True")]
        [HttpPost]
        public IActionResult AddHall(string title, string hallAddress, string hallLocation, string hallImage)
        {
            var _hallConetxt = HttpContext.RequestServices.GetService(typeof(HallContext)) as HallContext;
            var _hall = _hallConetxt.GetAllHalls().FirstOrDefault(h => h.HallAddress == hallAddress && h.HallLocation == hallLocation);

            if(_hall != null)
            {
                ViewData["Message"] = "По этому адресу в указанном месте уже есть зал";
                return View();
            }

            _hallConetxt.AddHall(title, hallAddress, hallLocation, hallImage);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "True")]
        public IActionResult Edit(int id)
        {
            var _hallContext = HttpContext.RequestServices.GetService(typeof(HallContext)) as HallContext;
            var _imageContext = HttpContext.RequestServices.GetService(typeof(FileContext)) as FileContext;

            var _editHallContext = HttpContext.RequestServices.GetService(typeof(EditHallContext)) as EditHallContext;

            return View(_editHallContext.GetData(_hallContext.GetAllHalls().FirstOrDefault(e => e.Id == id), _imageContext.GetData()));
        }

        [Authorize(Roles = "True")]
        [HttpPost]
        public IActionResult Edit(int id, string title, string hallAddress, string hallLocation, string hallImage)
        {
            var _hallContext = HttpContext.RequestServices.GetService(typeof(HallContext)) as HallContext;
            _hallContext.Edit(id, title, hallAddress, hallLocation, hallImage);

            return RedirectToAction("Index");
        }
    }
}
