using System;
using System.ComponentModel.DataAnnotations;

namespace SaaSPro.Services.ViewModels
{
    public class PlansUpdateModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Name", Description = "The name of the plan.")]
        public string Name { get; set; }

        [Required]
        [Display(Description = "The price of the plan")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Period", Description = "Not sure")]
        public string Period { get; set; }

        [Required]
        [Display(Name = "Order Index", Description = "Order index of the plan for sorting purposes")]
        public int OrderIndex { get; set; }

        [Required]
        [Display(Name = "Plan Code", Description = "The Stripe Plan code.")]
        public string PlanCode { get; set; }

        [Required]
        [Display(Name = "Enabled", Description = "Is the plan nabled?")]
        public bool Enabled { get; set; }
    }
}