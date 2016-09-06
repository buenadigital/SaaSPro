using System.ComponentModel.DataAnnotations;

namespace SaaSPro.Web.ViewModels
{
    public class AccountChangePasswordModel
    {
        [Required]
        [Display(Name = "Current Password")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }
        
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