using System.ComponentModel.DataAnnotations;

namespace SaaSPro.Web.ViewModels
{
    public class AuthLogInModel
    {
        [Required(ErrorMessage = "Your email address is required.")]
        [EmailAddress(ErrorMessage = "The email address you entered is invalid.")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "A password is required")]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}