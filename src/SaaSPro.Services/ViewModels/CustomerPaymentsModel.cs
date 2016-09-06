using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Stripe;

namespace SaaSPro.Services.ViewModels
{
    public class CustomerPaymentsModel
    {
        public Guid CustomerId { get; set; }
        
        [Display(Name = "Customer")]
        public String CustomerName { get; set; }

        [Display(Name = "Email")]
        public String CustomerEmail { get; set; }

        [Display(Name = "Stripe id")]
        public String PaymentCustomerId { get; set; }

        public StripePlan CustomerPlan { get; set; }

        public IEnumerable<StripeCharge> StripeChargesList { get; set; }
        
    }
}