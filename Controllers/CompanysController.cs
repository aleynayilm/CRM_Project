using CRMV2.Models;
using CRMV2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Runtime.CompilerServices;
using log4net;
using System.Reflection;
using log4net.Config;
using CRMV2.Ops;

namespace CRMV2.Controllers
{
    public class CompanysController : Controller
    {
        AleyLog aleyLog = new AleyLog();

        private readonly CrmContext _context;
        public CompanysController(CrmContext context)
		{
			_context = context;

        }
        public IActionResult Index()
        {
            List<Company> companies = _context.Companies.ToList();
            try
            {
                aleyLog.Write(AleyLog.Level.Info, "Company index page accessed.", User);
                User.Claims.FirstOrDefault();
                return View(companies);
            }
            catch (Exception ex) {
                aleyLog.Write(AleyLog.Level.Warn, $"An issue occurred while accessing the company index page: {ex.Message}", User);
                aleyLog.Write(AleyLog.Level.Error, $"Error while accessing the company index page: {ex.Message}", User);
                return View("Error");
            }
        }
        [HttpPost]
        public IActionResult Index(CompanyVM companyVM)
        {
            using (CrmContext context = new CrmContext())
            {
                List<Company> companies = _context.Companies.ToList();
                //log.Info("Index POST action: List of companies retrieved.");
                return View(companies);
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            //log.Info("Create GET action: Create view requested.");
            return View();
        }
        [HttpPost]
        public IActionResult Create(CompanyVM companyVM)
        {
            if (ModelState.IsValid)
            {
                var company = new Mappers().CompanyMap(companyVM);
                
                try
                {
                    _context.Companies.Add(company);
                    _context.SaveChanges();
                    aleyLog.Write(AleyLog.Level.Info, $"Company '{company.Name}' created.", User);
                    User.Claims.FirstOrDefault();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    aleyLog.Write(AleyLog.Level.Warn, $"An issue occurred while the creating the company: {ex.Message}", User);
                    aleyLog.Write(AleyLog.Level.Error, $"Error while the creating the company: {ex.Message}", User);
                    return View(companyVM);
                }
                //log.Info($"Create POST action: Company '{company.Name}' created.");
            }
            return View(companyVM);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var company = _context.Companies.Find(id);
            if (company == null)
            {
                //log.Warn($"Delete GET action: Company with id {id} not found.");
                ViewBag.Mesaj = "Şirket bulunamadı.";
            }
            else { //log.Info($"Delete GET action: Delete view requested for company id {id}.");
                   }

            return View(company);
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var company = _context.Companies.Find(id);
            if (company == null)
            {
                aleyLog.Write(AleyLog.Level.Warn, $"An issue occurred while the deleting the company", User);
                //log.Warn($"DeleteConfirmed POST action: Company with id {id} not found.");
                ViewBag.Mesaj = "Silinecek öge bulunamadı.";
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    _context.Companies.Remove(company);
                    _context.SaveChanges();
                    aleyLog.Write(AleyLog.Level.Info, $"Company '{company.Name}' deleted.", User);
                    User.Claims.FirstOrDefault();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    aleyLog.Write(AleyLog.Level.Error, $"Error while the deleting the company: {ex.Message}", User);
                    return View("Error");
                }
                //log.Info($"DeleteConfirmed POST action: Company with id {id} deleted.");
            }
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var company = _context.Companies.Find(id);
            if (company == null)
            {
                //log.Warn($"Update GET action: Company with id {id} not found.");
                TempData["Error"] = "Güncellenecek öğe bulunamadı.";
                return RedirectToAction("Index");
            }
            var companyVM = new CompanyVM
            {
                Id = company.Id,
                CompanyName = company.Name,
                Address = company.Adress,
                Email = company.Email
            };
            //log.Info($"Update GET action: Update view requested for company id {id}.");
            return View(companyVM);
        }
        [HttpPost]
        public IActionResult Update(CompanyVM companyVM) 
        {
            var existingCompany = _context.Companies.Find(companyVM.Id);
                {
				if (existingCompany == null)
				{
                    aleyLog.Write(AleyLog.Level.Warn, $"An issue occurred while the updating the company", User);
                    //log.Warn($"Update POST action: Company with id {companyVM.Id} not found.");
                    TempData["Error"] = "Güncellenecek öğe bulunamadı.";
					return RedirectToAction("Index");
				}
                existingCompany.Name = companyVM.CompanyName;
                existingCompany.Adress = companyVM.Address;
                existingCompany.Email=companyVM.Email;
                try
                {
                    _context.Companies.Update(existingCompany);
                    _context.SaveChanges();
                    aleyLog.Write(AleyLog.Level.Info, $"Company '{companyVM.CompanyName}' updating.", User);
                    User.Claims.FirstOrDefault();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    aleyLog.Write(AleyLog.Level.Error, $"Error while the updating the company: {ex.Message}", User);
                    return View("Error");
                }
                //log.Info($"Update POST action: Company with id {companyVM.Id} updated.");
			}
        }
    }
}
