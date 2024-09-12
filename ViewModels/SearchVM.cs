using CRMV2.Models;
using log4net;

namespace CRMV2.ViewModels
{
    public class SearchVM
    {
        public string? SearchTerm { get; set; }
        public List<Log>? Logs { get; set; }
    }
}
