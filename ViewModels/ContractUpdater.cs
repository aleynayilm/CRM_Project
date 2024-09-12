using CRMV2.Models;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace CRMV2.ViewModels
{
    public class ContractUpdater
    {
        private readonly CrmContext _context;

        public ContractUpdater(CrmContext context)
        {
            _context = context;
        }

        public void UpdateExpiredContracts()
        {
            var currentDate = DateTime.Today;

            var expiredContracts = _context.Contracts
                .Where(c => c.EndDate == currentDate && c.Active==true)
                .ToList();

            foreach (var contract in expiredContracts)
            {
                contract.Active = false;
                SendEmailAuto(contract);
            }

            _context.SaveChanges();
        }
        public void SendEmailAuto(Contract contract)
        {
            var contactPersons = _context.ContactPeople.Where(cp => cp.ContractId == contract.Id).ToList();
            foreach(var contactPerson in contactPersons)
            {
                var mailMessage = new MailMessage("alynylmazz@gmail.com", contactPerson.Email)
                {
                    Subject = "Contract Expired",
                    Body = $"Dear {contactPerson.Name},\n\nThe contract with ID {contract.Id} has expired."
                };

                using (var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("alynylmazz@gmail.com", "lgoi bjjk vjvl vrre"),
                    EnableSsl = true,
                })
                {
                    smtpClient.Send(mailMessage);
                }
            }
        }
    }

}
