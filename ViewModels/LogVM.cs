using CRMV2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRMV2.ViewModels
{
    public class LogVM
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string? Thread { get; set; }

        public string? Level { get; set; }

        public string? Logger { get; set; }

        public string? Message { get; set; }

        public int? UserId { get; set; }
        public IEnumerable<Log> Logs { get; set; }
        public string UserFirstName { get; set; }
        public IEnumerable<SelectListItem>? Users { get; set; }
        public string? SelectedUser { get; set; }
    }
}
