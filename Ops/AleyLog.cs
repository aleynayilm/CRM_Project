using CRMV2.Models;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Security.Claims;

namespace CRMV2.Ops
{
    public class AleyLog
    {
        public string Email { get; set; }
        public bool Write(Level level, string msg, System.Security.Claims.ClaimsPrincipal user)
        {
            
            using (CrmContext _context = new CrmContext())
            {
                string email = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
                string userId = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                Debug.WriteLine(email);
                Debug.WriteLine(msg);

                var log = new Log
                {
                    Date = DateTime.Now,
                    Thread = Thread.CurrentThread.ManagedThreadId.ToString(),
                    Level = level.ToString(),
                    Logger = nameof(AleyLog),
                    Message = msg,
                    UserId = int.Parse(userId)
                };

                _context.Logs.Add(log);
                _context.SaveChanges();
            }
            
            return true;
        }

        public enum Level
        {
            Info,
            Warn,
            Error
        }
    }
}
