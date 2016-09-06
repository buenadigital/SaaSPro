using System;
using System.ComponentModel.DataAnnotations;

namespace SaaSPro.Services.ViewModels
{
    public class UsersResetPasswordModel
    {
        public Guid Id { get; set; }

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