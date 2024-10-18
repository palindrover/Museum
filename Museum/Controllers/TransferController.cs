using Museum.Contexts;
using Museum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Museum.Controllers
{
    public class TransferController : Controller
    {
        private TransferContext context;

        [Authorize(Roles = "True")]
        public IActionResult Index()
        {
            context = HttpContext.RequestServices.GetService(typeof(TransferContext)) as TransferContext;

            return View(context.GetAllTransfers());
        }

        [Authorize(Roles = "True")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            context.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
