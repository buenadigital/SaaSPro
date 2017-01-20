using System.ComponentModel.DataAnnotations;

namespace SaaSPro.Services.ViewModels
{
    public class ContactUsSupportModel
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Description = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Display(Description = "Phone")]
        [DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string Phone { get; set; }

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
