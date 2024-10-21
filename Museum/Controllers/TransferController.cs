using Museum.Contexts;
using Museum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Museum.Controllers
{
    public class TransferController : Controller
    {
        [Authorize(Roles = "True")]
        public IActionResult Index()
        {
            var _context = HttpContext.RequestServices.GetService(typeof(TransferContext)) as TransferContext;

            return View(_context.GetAllTransfers());
        }

        [Authorize(Roles = "True")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
			var _context = HttpContext.RequestServices.GetService(typeof(TransferContext)) as TransferContext;
			_context.Delete(id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "True")]
        public IActionResult Details(int id)
        {
			var _context = HttpContext.RequestServices.GetService(typeof(TransferContext)) as TransferContext;
			return View(_context.GetFullTransfer(id));
        }

        [Authorize(Roles = "True")]
        public IActionResult Edit(int id)
        {
            var _context = HttpContext.RequestServices.GetService(typeof(TransferContext)) as TransferContext;
            return View(_context.GetFullTransfer(id));
        }

        [Authorize(Roles = "True")]
        [HttpPost]
        public IActionResult Edit(int id, string sender, string transferDate, string returns, string docNum, string address)
        {
            var _context = HttpContext.RequestServices.GetService(typeof(TransferContext)) as TransferContext;
            _context.Edit(id, sender, transferDate, returns, docNum, address);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "True")]
        [HttpGet]
        public IActionResult Add()
        {
            var _exhibitsContext = HttpContext.RequestServices.GetService(typeof(ExhibitContext)) as ExhibitContext;
            var _contractorsContext = HttpContext.RequestServices.GetService(typeof(ContractorContext)) as ContractorContext;
            var _addContext = HttpContext.RequestServices.GetService(typeof(AddTransferContext)) as AddTransferContext;
            return View(_addContext.GetData(_exhibitsContext.GetAllExhibits(), _contractorsContext.GetAllContractors()));
        }

        [Authorize(Roles = "True")]
        [HttpPost]
        public IActionResult Add(int exhibitid, string sender, string transferdate, string purpose, string returns, string docnum, string address, int contractorid)
        {
            var _transferContext = HttpContext.RequestServices.GetService(typeof(TransferContext)) as TransferContext;
            var _exhibitContext = HttpContext.RequestServices.GetService(typeof(ExhibitContext)) as ExhibitContext;

            var _exhibit = _exhibitContext.GetExhibitById(exhibitid);
            if(_exhibit.WhereTransmittedId > 0)
            {
                ViewData["Message"] = "Ошибка, экспонат уже отправлен другому контрагенту";
                return View();
            }

            int _transferid = _transferContext.Add(sender, transferdate, purpose, returns, docnum, address, contractorid);
            _exhibitContext.SetTransfer(exhibitid, _transferid);

            return RedirectToAction("Index");
        }
    }
}
