using System;
using System.ComponentModel.DataAnnotations;

namespace SaaSPro.Services.ViewModels
{
    public class RefundModel
    {
        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public string PaymentId { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = "Amount is required")]
        public decimal Amount { get; set; }

        public int AmountCents
        {
            get { return (int) Math.Round(Amount * 100); }
        }
    }
}