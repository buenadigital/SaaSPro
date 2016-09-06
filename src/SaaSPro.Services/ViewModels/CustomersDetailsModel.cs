using System;
using System.ComponentModel.DataAnnotations;

namespace SaaSPro.Services.ViewModels
{
    public class CustomersDetailsModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [Display(Name = "Full name")]
        public string FullName { get; set; }

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

        [Required(ErrorMessage = "Hostname is required")]
        [Display(Name = "Hostname")]
        [MaxLength(128, ErrorMessage = "Max length 128 characters")]
        public string Hostname { get; set; }
        
        [Display(Name = "Enabled", Description = "Whether this Customer is enabled and can access the service.")]
        public bool Enabled { get; set; }

        public string PaymentCustomerId { get; set; }

        public bool HasPlan { get; set; }
    }
}