using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CRMV2.ViewModels
{
	public class ContactPersonVM
	{
        public int? Id { get; set; }
        public int? ContractId { get; set; }
        [Display(Name = "Contact Person Name")]
		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; } = null!;
		[Display(Name = "Contact Person Name")]
		[Required(ErrorMessage = "Last Name is required")]

		public string? LastName { get; set; }
		[Display(Name = "Phone Number")]
		[Required(ErrorMessage = "Phone Number is required")]

		public string? PhoneNumber { get; set; }
		[Display(Name = "Email")]
		[Required(ErrorMessage = "Email is required")]

		public string? Email { get; set; }
		public IEnumerable<SelectListItem>? Contractt { get; set; }
		public int? SelectedContractt { get; set; }
	}
}
