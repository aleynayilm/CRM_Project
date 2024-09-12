using System.ComponentModel.DataAnnotations;

namespace CRMV2.ViewModels
{
	public class CompanyVM
	{
		public int? Id { get; set; } 
		[Display(Name = "Company Name")]
		[Required(ErrorMessage = "Company Name is required")]
		public string? CompanyName { get; set; }
		[Display(Name = "Address")]
		[Required(ErrorMessage = "Address is required")]
		public string? Address { get; set; }
		[Display(Name = "Email")]
		[Required(ErrorMessage = "Email is required")]
		public string? Email { get; set; }
	}
}
