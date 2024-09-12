using CRMV2.Models;
using CRMV2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using log4net;
using System.Reflection;
using CRMV2.Ops;

namespace CRMV2.Controllers
{
    public class ContractsController : Controller
    {
        AleyLog aleyLog=new AleyLog();
      //  private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly CrmContext _context;
        public ContractsController(CrmContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            try
            {
                var contracts = _context.Contracts
                                .Include(c => c.Company)
                                .ToList();
                aleyLog.Write(AleyLog.Level.Info, "Contract index page accessed.", User);
                User.Claims.FirstOrDefault();
                return View(contracts);
            }
            catch (Exception ex)
            {
                aleyLog.Write(AleyLog.Level.Warn, $"An issue occurred while accessing the contract index page: {ex.Message}", User);
                aleyLog.Write(AleyLog.Level.Error, $"Error while accessing the contract index page: {ex.Message}", User);
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult Index(ContractVM contractVM)
        {
            using (CrmContext context = new CrmContext())
            {
                List<Contract> contracts = _context.Contracts.ToList();
             //   log.Info("Index POST action: List of contracts retrieved.");
                return View(contracts);
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            ContractVM contractVM = new ContractVM();
            var companies = _context.Companies.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            })
            .ToList();
            contractVM.Companies = companies;
            ViewBag.Companies = companies;
            var contractTypes = _context.ContactsTypes.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            })
            .ToList();
            contractVM.ContractTypes = contractTypes;
            //  log.Info("Create GET action: Create view requested.");
            return View(contractVM);
        }
        [HttpPost]
        public IActionResult Create(ContractVM contractVM)
        {
            //if (ModelState.IsValid)
            //{
                var contract = new Mappers().ContractMap(contractVM);
            try
            {
                _context.Contracts.Add(contract);
                _context.SaveChanges();
                aleyLog.Write(AleyLog.Level.Info, $"Contract '{contract.Name}' created.", User);
                User.Claims.FirstOrDefault();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                aleyLog.Write(AleyLog.Level.Warn, $"An issue occurred while the creating the contract: {ex.Message}", User);
                aleyLog.Write(AleyLog.Level.Error, $"Error while the creating the contract: {ex.Message}", User);
                return View(contractVM);
            }
            // log.Info($"Create POST action: Contract '{contract.Name}' created.");
            ViewBag.Mesaj = "Sözlesme kaydedildi.";
                return RedirectToAction("Index");
            //}
            contractVM.Companies = _context.Companies.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            })
             .ToList();

            contractVM.ContractTypes = _context.ContactsTypes.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
          //  log.Warn("Create POST action: Model state is invalid.");
            return View(contractVM);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var contract = _context.Contracts.Find(id);
            if (contract == null)
            {
              //  log.Warn($"Delete GET action: Contract with id {id} not found.");
                ViewBag.Mesaj = "Sözleşme bulunamadı.";
            }
         //   log.Info($"Delete GET action: Delete view requested for contract id {id}.");
            return View(contract);
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var contract = _context.Contracts.Find(id);
            if (contract == null)
            {
                aleyLog.Write(AleyLog.Level.Warn, $"An issue occurred while the deleting the contract", User);
                //     log.Warn($"DeleteConfirmed POST action: Contract with id {id} not found.");
                ViewBag.Mesaj = "Silinecek sözleşme bulunamadı.";
                return RedirectToAction("Index");
            }
            try
            {
                _context.Contracts.Remove(contract);
                _context.SaveChanges();
                aleyLog.Write(AleyLog.Level.Info, $"Contract '{contract.Name}' deleted.", User);
                User.Claims.FirstOrDefault();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                aleyLog.Write(AleyLog.Level.Error, $"Error while the deleting the contract: {ex.Message}", User);
                return View("Error");
            }
            //  log.Info($"DeleteConfirmed POST action: Contract with id {id} deleted.");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var contract = _context.Contracts.Find(id);
            if (contract == null)
            {
             //   log.Warn($"Update GET action: Contract with id {id} not found.");
                TempData["Error"] = "Güncellenecek öğe bulunamadı.";
                return RedirectToAction("Index");
            }
            var contactVM = new ContractVM
            {
                Id = contract.Id,
                CompanyId = contract.CompanyId,
                Name = contract.Name,
                BeginDate = contract.BeginDate,
                EndDate = contract.EndDate,
                Active = contract.Active,
                SelectedContractType = contract.Type,
                SelectedCompany = contract.CompanyId,

                ContractTypes = _context.ContactsTypes.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList(),
                Companies = _context.Companies.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };
         //   log.Info($"Update GET action: Update view requested for contract id {id}.");
            return View(contactVM);
        }
        [HttpPost]
        public IActionResult Update(ContractVM contractVM)
        {
            var existingContract = _context.Contracts.Find(contractVM.Id);
            {
                if (existingContract == null)
                {
                    aleyLog.Write(AleyLog.Level.Warn, $"An issue occurred while the updating the contract", User);
                    //   log.Warn($"Update POST action: Contract with id {contractVM.Id} not found.");
                    TempData["Error"] = "Güncellenecek öğe bulunamadı.";
                    return RedirectToAction("Index");
                }
                existingContract.Name = contractVM.Name;
                existingContract.BeginDate = contractVM.BeginDate;
                existingContract.EndDate = contractVM.EndDate;
                existingContract.Active = contractVM.Active;
                try
                {
                    _context.Contracts.Update(existingContract);
                    _context.SaveChanges();
                    aleyLog.Write(AleyLog.Level.Info, $"Contract '{contractVM.Name}' updating.", User);
                    User.Claims.FirstOrDefault();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    aleyLog.Write(AleyLog.Level.Error, $"Error while the updating the contract: {ex.Message}", User);
                    return View("Error");
                }
                //    log.Info($"Update POST action: Contract with id {contractVM.Id} updated.");
            }
        }

    }
}
