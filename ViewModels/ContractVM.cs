using CRMV2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CRMV2.ViewModels
{
	public class ContractVM
	{
        public int Id { get; set; }

        public int CompanyId { get; set; }
        public int? Type { get; set; }
        [Display(Name = "Contract Name")]
        [Required(ErrorMessage = "Name is required")]

        public string? Name { get; set; }
        [Display(Name = "Begin Date")]
        [Required(ErrorMessage = "Begin Date is required")]

        public DateTime? BeginDate { get; set; }
        [Display(Name = "End Date")]
        [Required(ErrorMessage = "End Date is required")]

        public DateTime? EndDate { get; set; }
        [Display(Name = "Is Active")]
        [Required(ErrorMessage = "End Date can not be past")]

        public bool? Active { get; set; }
        [Display(Name = "Contract Type")]
        [Required(ErrorMessage = "Type is required")]

		public IEnumerable<SelectListItem>? Companies { get; set; }
        public int? SelectedCompany { get; set; }
		public IEnumerable<SelectListItem>? ContractTypes { get; set; }
		public int? SelectedContractType { get; set; }
    }
}
