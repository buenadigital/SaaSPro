using System.ComponentModel.DataAnnotations;

namespace SaaSPro.Web.ViewModels
{
    public class AuthResetPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string ResetToken { get; set; }

        [Required]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}