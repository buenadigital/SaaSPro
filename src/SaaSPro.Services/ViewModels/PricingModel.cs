using System.Collections.Generic;
using SaaSPro.Domain;

namespace SaaSPro.Services.ViewModels
{
    public class PricingModel
    {
        public IList<Plan> Plans { get; set; }
        public IList<PlanInfo> PlanInfoItems { get; set; }
        public IList<PlanInfoValue> PlanInfoValues { get; set; }
    }
}