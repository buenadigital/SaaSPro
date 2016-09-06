using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SaaSPro.Services.ViewModels
{
    public class PlanSetupModel
    {
        public string CustomerId { get; set; }
        public string PaymentCustomerId { get; set; }
        public SelectList StripePlanList { get; set; }
        public SelectList MonthsList { get; set; }
        public SelectList YearsList { get; set; }

        [Display(Name = "Plan")]
        [Required(ErrorMessage = "Required")]
        public string StripePlanId { get; set; }

        [Display(Name = "Card number")]
        [Required(ErrorMessage = "Required")]
        [RegularExpression(
            @"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$"
            , ErrorMessage = "Enter a valid card number")]
        public string CreditCardNumber { get; set; }

        [Display(Name = "Exp. month")]
        [Required(ErrorMessage = "Required")]
        public int CardExpirationMonth { get; set; }

        [Display(Name = "Exp. year")]
        [Required(ErrorMessage = "Required")]
        public int CardExpirationYear { get; set; }

        [Display(Name = "CVC"), MaxLength(4), MinLength(3)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Enter valid CVC")]
        public string Cvc { get; set; }
    }
}