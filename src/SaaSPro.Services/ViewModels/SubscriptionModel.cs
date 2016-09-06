using System.ComponentModel.DataAnnotations;

namespace SaaSPro.Services.ViewModels
{
    public class SubscriptionModel
    {
        [Display(Name = "Plan")]
        public string CurrentPlan { get; set; }
        public string CurrentPeriod { get; set; }
        [Display(Name = "Price")]
        public decimal CurrentPrice { get; set; }
    }
}