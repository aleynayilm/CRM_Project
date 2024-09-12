using System.ComponentModel.DataAnnotations;

namespace CRMV2.ViewModels
{
	public class UserVM
	{
		[Display(Name = "User Id")]
		[Required(ErrorMessage = "User Id is required")]
		public int Id { get; set; }
		[Display(Name = "User Name")]
		[Required(ErrorMessage = "User Name is required")]

		public string? FirstName { get; set; }
		[Display(Name = "User Last Name")]
		[Required(ErrorMessage = "User Last Name is required")]

		public string? LastName { get; set; }
		[Display(Name = "User Email")]
		[Required(ErrorMessage = "User Email is required")]

		public string? Email { get; set; }
		[Display(Name = "Password")]
		[Required(ErrorMessage = "Password is required")]

		public string? Password { get; set; }
	}
}
