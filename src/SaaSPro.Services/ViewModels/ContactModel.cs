using System.ComponentModel.DataAnnotations;

namespace SaaSPro.Services.ViewModels
{
    public class ContactModel
    {
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        [MaxLength(50, ErrorMessage = "Max length 50 characters")]
        [RegularExpression("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*", ErrorMessage = "Email format is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Message is required")]
        [Display(Description = "Message")]
        public string Message { get; set; }

        public string MailTo { get; set; }
    }
}
