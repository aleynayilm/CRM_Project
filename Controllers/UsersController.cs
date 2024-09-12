using CRMV2.Models;
using CRMV2.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CRMV2.Controllers
{
    [AllowAnonymous]
    public class UsersController : Controller
	{
		private readonly CrmContext _context;

		public UsersController(CrmContext context)
		{
			_context = context;
		}
        [HttpGet("{id}")]
        public IActionResult Index(int id)
        {
            var user = _context.Users.Find(id);
            return Ok(user);
        }
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.Users
                .Select(u => new { id = u.Id, text = u.FirstName })
                .ToList();
            return Ok(users);
        }

        [HttpGet]
		public IActionResult Login() => View();
		
		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> LoginAsync(UserVM userVM)
		{
			var user = _context.Users.Where(x => x.Email == userVM.Email && x.Password == userVM.Password).FirstOrDefault();
			{
				if (user == null)
				{
					TempData["Error"] = "User not found.";
					return View();
				}
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                var claims = new List<Claim> {
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
				};
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }
		}
		[HttpPost]
		public async Task<IActionResult> LogoutAsync() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData.Clear();
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            return RedirectToAction("Login", "Users");
        }
	}
}
