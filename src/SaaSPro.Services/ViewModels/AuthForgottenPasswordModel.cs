using System.ComponentModel.DataAnnotations;

namespace SaaSPro.Services.ViewModels
{
    public class AuthForgottenPasswordModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email", Description = "Your registered email address.")]
        public string Email { get; set; }

        public string ResetPasswordUrl { get; set; }
    }
}