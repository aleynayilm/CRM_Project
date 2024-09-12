using CRMV2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CRMV2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace CRMV2.Controllers
{
    [Authorize]
    public class HomeController : Controller
	{
        private readonly CrmContext _context;

        public HomeController(CrmContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}

	}	
}
