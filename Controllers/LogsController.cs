using CRMV2.Models;
using CRMV2.Ops;
using CRMV2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;

namespace CRMV2.Controllers
{
    public class LogsController : Controller
    {
        private readonly CrmContext _context;

        public LogsController(CrmContext context)
        {
            _context = context;
        }
        [HttpGet("api/logs")]
        public IActionResult GetLogs()
        {
            var logs = _context.Logs
                .Include(log => log.User)
                .Select(log => new LogVM
                {
                    Date = log.Date,
                    Level = log.Level,
                    Logger = log.Logger,
                    Message = log.Message,
                    UserFirstName = log.User.FirstName
                })
                .ToList();

            return Json(logs);
        }

        public IActionResult Index()
        {
            var users = _context.Users.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.FirstName
            }).ToList();

            ViewBag.Users = users;
            var logs = _context.Logs
                .Include(log => log.User)
                .Select(log => new LogVM
                {
                    Date = log.Date,
                    Level = log.Level,
                    Logger = log.Logger,
                    Message = log.Message,
                    UserFirstName = log.User.FirstName
                }).ToList();
            return View(logs);
        }
        [HttpPost]
        public IActionResult Index(LogVM logVM)
        {
            using (CrmContext context = new CrmContext())
            {
                List<Log> logs = _context.Logs.ToList();
                return View(logs);
            }
        }
        [HttpGet]
        public IActionResult Filter(string? selectedUser, DateTime? beginDate, DateTime? endDate)
        {
            var users = _context.Users.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.FirstName
            }).ToList();

            ViewBag.Users = users;

            var logsQuery = _context.Logs.AsQueryable();

            if (!string.IsNullOrEmpty(selectedUser))
            {
                var userId = int.Parse(selectedUser);
                logsQuery = logsQuery.Where(log => log.UserId == userId);
            }
            if (beginDate.HasValue)
            {
                logsQuery = logsQuery.Where(log => log.Date >= beginDate.Value);
            }
            if (endDate.HasValue)
            {
                logsQuery = logsQuery.Where(log => log.Date <= endDate.Value);
            }
            var logs = logsQuery
                .Include(log => log.User)
                .Select(log => new LogVM
                {
                    Date = log.Date,
                    Level = log.Level,
                    Logger = log.Logger,
                    Message = log.Message,
                    UserFirstName = log.User.FirstName,
                    Users = users,
                    SelectedUser = selectedUser,
                }).ToList();
            return View("Index", logs);
        }
    }
}
