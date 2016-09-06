using System.ComponentModel.DataAnnotations;
using SaaSPro.Web.Common.Attributes;

namespace SaaSPro.Services.ViewModels
{
    public class SignUpModel
    {
        [Display(Name = "Plan name")]
        public string PlanName { get; set; }

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

        [Required(ErrorMessage = "Domain is required")]
        [Display(Name = "Domain")]
        [MaxLength(128, ErrorMessage = "Max length 128 characters")]
        public string Domain { get; set; }

        // Credit Card
        [Required(ErrorMessage = "Card number is required")]
        [StringLength(16, MinimumLength = 12, ErrorMessage = "Card number is invalid")]
        [CreditCardNumber(ErrorMessage = "Card number is invalid")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Security code is required")]
        [RegularExpression(@"\d{3,4}", ErrorMessage = "Security code is invalid")]
        public string SecurityCode { get; set; }

        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }

        [Required(ErrorMessage = "Expiration is required")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "Expiration length is invalid")]
        public string Expiration { get; set; }

        public string FullName => FirstName + " " + LastName;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 7)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
}