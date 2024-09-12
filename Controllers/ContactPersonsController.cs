using CRMV2.Models;
using CRMV2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using log4net;
using System.Reflection;
using CRMV2.Ops;

namespace CRMV2.Controllers
{
    public class ContactPersonsController : Controller
    {
        // private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        AleyLog aleyLog = new AleyLog();
        private readonly CrmContext _context;
        public ContactPersonsController(CrmContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<ContactPerson> contactPersons = _context.ContactPeople.ToList();
            try
            {
                aleyLog.Write(AleyLog.Level.Info, "Contact Person index page accessed.", User);
                User.Claims.FirstOrDefault();
                return View(contactPersons);
            }
            catch (Exception ex)
            {
                aleyLog.Write(AleyLog.Level.Warn, $"An issue occurred while accessing the contact person index page: {ex.Message}", User);
                aleyLog.Write(AleyLog.Level.Error, $"Error while accessing the contact person index page: {ex.Message}", User);
                return View("Error");
            }
        }
        [HttpPost]
        public IActionResult Index(ContactPersonVM contactPersonVM)
        {
            using (CrmContext context = new CrmContext())
            {
                List<ContactPerson> contactPeople = _context.ContactPeople.ToList();
               // log.Info("Index POST action: List of contact persons retrieved.");
                return View(contactPeople);
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
			ContactPersonVM contactPersonVM = new ContactPersonVM();
			var contactPerson = _context.Contracts.Select(c => new SelectListItem
			{
				Value = c.Id.ToString(),
				Text = c.Name
			})
		   .ToList();
			contactPersonVM.Contractt = contactPerson;
			ViewBag.Contracts = contactPerson;
           // log.Info("Create GET action: Create view requested.");
            return View(contactPersonVM);
		}
        [HttpPost]
        public IActionResult Create(ContactPersonVM contactPersonVM)
        {
            //if (ModelState.IsValid)
            //{
                var contactPerson = new Mappers().ContactPersonMap(contactPersonVM);
            try
            {
                _context.ContactPeople.Add(contactPerson);
                _context.SaveChanges();
                aleyLog.Write(AleyLog.Level.Info, $"Contact person '{contactPerson.Name}' created.", User);
                User.Claims.FirstOrDefault();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                aleyLog.Write(AleyLog.Level.Warn, $"An issue occurred while the creating the contact person: {ex.Message}", User);
                aleyLog.Write(AleyLog.Level.Error, $"Error while the creating the contact person: {ex.Message}", User);
                return View(contactPersonVM);
            }

            //  log.Info($"Create POST action: Contact person '{contactPerson.Name} {contactPerson.LastName}' created.");
            ViewBag.Mesaj = "Kişi başarıyla kaydedildi.";
                return RedirectToAction("Index");
            // }
          //  log.Warn("Create POST action: Model state is invalid.");
            return View(contactPersonVM);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var contactPerson = _context.ContactPeople.Find(id);
            if (contactPerson == null)
            {
              //  log.Warn($"Delete GET action: Contact person with id {id} not found.");
                ViewBag.Mesaj = "Kişi bulunamadı.";
            }
           // log.Info($"Delete GET action: Delete view requested for contact person id {id}.");
            return View(contactPerson);
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var contactPerson = _context.ContactPeople.Find(id);
            if (contactPerson == null)
            {
                aleyLog.Write(AleyLog.Level.Warn, $"An issue occurred while the deleting the contact person", User);
                // log.Warn($"DeleteConfirmed POST action: Contact person with id {id} not found.");
                ViewBag.Mesaj = "Silinecek kişi bulunamadı.";
                return RedirectToAction("Index");
            }
            try
            {
                _context.ContactPeople.Remove(contactPerson);
                _context.SaveChanges();
                aleyLog.Write(AleyLog.Level.Info, $"Contact person '{contactPerson.Name}' deleted.", User);
                User.Claims.FirstOrDefault();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                aleyLog.Write(AleyLog.Level.Error, $"Error while the deleting the contact person: {ex.Message}", User);
                return View("Error");
            }
            //  log.Info($"DeleteConfirmed POST action: Contact person with id {id} deleted.");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var contactPerson = _context.ContactPeople.Find(id);
            if (contactPerson == null)
            {
               // log.Warn($"Update GET action: Contact person with id {id} not found.");
                TempData["Error"] = "Güncellenecek öğe bulunamadı.";
                return RedirectToAction("Index");
            }
            var contactPersonVM = new ContactPersonVM
            {
                Id = contactPerson.Id,
                Name = contactPerson.Name,
                LastName = contactPerson.LastName,
                PhoneNumber = contactPerson.PhoneNumber,
                Email = contactPerson.Email,
                ContractId= contactPerson.ContractId,
                SelectedContractt = contactPerson.ContractId,

                Contractt = _context.Contracts.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };
          //  log.Info($"Update GET action: Update view requested for contact person id {id}.");
            return View(contactPersonVM);
        }
        [HttpPost]
        public IActionResult Update(ContactPersonVM contactPersonVM)
        {
            var existingContactPerson = _context.ContactPeople.Find(contactPersonVM.Id);
            {
                if (existingContactPerson == null)
                {
                    aleyLog.Write(AleyLog.Level.Warn, $"An issue occurred while the updating the contact person", User);
                    //     log.Warn($"Update POST action: Contact person with id {contactPersonVM.Id} not found.");
                    TempData["Error"] = "Güncellenecek öğe bulunamadı.";
                    return RedirectToAction("Index");
                }
                existingContactPerson.Name = contactPersonVM.Name;
                existingContactPerson.LastName = contactPersonVM.LastName;
                existingContactPerson.PhoneNumber = contactPersonVM.PhoneNumber;
                existingContactPerson.Email = contactPersonVM.Email;
                try
                {
                    _context.ContactPeople.Update(existingContactPerson);
                    _context.SaveChanges();
                    aleyLog.Write(AleyLog.Level.Info, $"Contact person '{contactPersonVM.Name}' updating.", User);
                    User.Claims.FirstOrDefault();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    aleyLog.Write(AleyLog.Level.Error, $"Error while the updating the contact person: {ex.Message}", User);
                    return View("Error");
                }
                //  log.Info($"Update POST action: Contact person with id {contactPersonVM.Id} updated.");
            }
        }
    }
}
