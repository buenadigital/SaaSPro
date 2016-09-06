using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SaaSPro.Domain;
using SaaSPro.Web.Common.Attributes;

namespace SaaSPro.Services.ViewModels
{
    public class ChangeSubscriptionModel
    {
        public IEnumerable<Plan> Plans { get; set; }

        [Display(Name = "Plan")]
        [Required(ErrorMessage = "Plan is required")]
        public string PlanId { get; set; }

        [Display(Name = "Credit Card")]
        [Required(ErrorMessage = "Card number is required")]
        [StringLength(20, ErrorMessage = "Card number is invalid")]
        [CreditCardNumber(ErrorMessage = "Card number is invalid")]
        public string CardNumber { get; set; }

        [Display(Name = "Security Code")]
        [Required(ErrorMessage = "Security code is required")]
        [RegularExpression(@"\d{3,4}", ErrorMessage = "Security code is invalid")]
        public string SecurityCode { get; set; }

        [Display(Name = "Expiration Month")]
        [Required(ErrorMessage = "Expiration month is required")]
        [Range(1, 12, ErrorMessage = "Expiration month must be from 1 to 12")]
        public int ExpirationMonth { get; set; }

        [Display(Name = "Expiration Year")]
        [Required(ErrorMessage = "Expiration year is required")]
        public int ExpirationYear { get; set; }
    }
}