using System.ComponentModel.DataAnnotations;

namespace SaaSPro.Services.ViewModels
{
    public class CustomersProvisionModel
    {
        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Company is required")]
        [Display(Name = "Company")]
        public string Company { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        [MaxLength(50, ErrorMessage = "Max length 50 characters")]
        [RegularExpression("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*", ErrorMessage = "Email format is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is required")]
        [Display(Name = "Password confirmation")]
        [Compare("Password", ErrorMessage = "Password and confirmation must be similar")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Domain is required")]
        [Display(Name = "Domain")]
        [MaxLength(128, ErrorMessage = "Max length 128 characters")]
        public string Domain { get; set; }
    }
}