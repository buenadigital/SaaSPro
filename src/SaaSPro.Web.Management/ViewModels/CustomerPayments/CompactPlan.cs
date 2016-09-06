using System;
using Stripe;

namespace SaaSPro.Web.Management.ViewModels.CustomerPayments
{
    public class CompactPlan
    {
        public string PlanId { get; private set; }
        public string PlanName { get; private set; }

        public CompactPlan(StripePlan plan)
        {
            PlanId = plan.Id;
            PlanName = String.Format("{0} ({1:0.00} {3}/{2})", plan.Name, FormatAmount(plan.Amount), plan.Interval,
                                     plan.Currency);
        }

        private static decimal FormatAmount(int? amount)
        {
            if (amount == null)
                return 0;
            return (amount > 0) ? ((decimal)amount / 100) : (decimal)0.00;
        }
    }
}