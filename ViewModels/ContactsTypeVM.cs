using System.ComponentModel.DataAnnotations;

namespace CRMV2.ViewModels
{
    public class ContactsTypeVM
    {
        [Display(Name = "Contract Type Name")]
        [Required(ErrorMessage = "Contract Type is required")]
        public string Name { get; set; } = null!;
    }
}
