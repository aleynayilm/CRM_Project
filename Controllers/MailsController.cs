using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace CRMV2.Controllers
{
    public class MailsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void SendEmail(string toEmailAddress, string subject, string body)
        {
            var mailMessage = new MailMessage("alynylmazz@gmail.com", toEmailAddress)
            {
                Subject = subject,
                Body = body
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
