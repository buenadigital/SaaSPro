using System.ComponentModel.DataAnnotations;

namespace SaaSPro.Web.API.Model.Auth
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Your email address is required.")]
        [EmailAddress(ErrorMessage = "The email address you entered is invalid.")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "A password is required")]
        public string Password { get; set; }
    }
}