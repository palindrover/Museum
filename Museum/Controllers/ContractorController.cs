using Microsoft.AspNetCore.Mvc;
using Museum.Contexts;
using Museum.Models;
using Microsoft.AspNetCore.Authorization;

namespace Museum.Controllers
{
    public class ContractorController : Controller
    {
        [Authorize(Roles = "True")]
        public IActionResult Index()
        {
            var _contractorsContext = HttpContext.RequestServices.GetService(typeof(ContractorContext)) as ContractorContext;

            return View(_contractorsContext.GetAllContractors());
        }
        
        [Authorize(Roles = "True")]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "True")]
        [HttpPost]
        public IActionResult Add(string company, string surname, string name, string patrname, string tel, string email)
        {
            var _contrContext = HttpContext.RequestServices.GetService(typeof(ContractorContext)) as ContractorContext;

            var _contractor =_contrContext.GetAllContractors().FirstOrDefault(c => c.Companyname==company && c.Surname==surname && c.Name==name && c.Patrname==patrname);
            if (_contractor != null) 
            {
                ViewData["Message"] = "Ошибка, контрагент существует";
                return View();
            }

            _contrContext.Add(company, surname, name, patrname, tel, email);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "True")]
        public IActionResult Edit(int id)
        {
            var _contr = HttpContext.RequestServices.GetService(typeof(ContractorContext)) as ContractorContext;
            return View(_contr.GetAllContractors().FirstOrDefault(_c => _c.Id==id));
        }

        [Authorize(Roles = "True")]
        [HttpPost]
        public IActionResult Edit(int id, string companyname, string surname, string name, string patrname, string tel, string email)
        {
            var _contractorContext = HttpContext.RequestServices.GetService(typeof(ContractorContext)) as ContractorContext;
            _contractorContext.Edit(id, companyname, surname, name,patrname, tel, email);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "True")]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var _contractorContext = HttpContext.RequestServices.GetService(typeof(ContractorContext)) as ContractorContext;
            _contractorContext.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
