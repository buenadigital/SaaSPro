using System;
using SaaSPro.Services.ViewModels;

namespace SaaSPro.Services.Messaging.SubscriptionsService
{
    public class ChangeSubscriptionRequest
    {
        public string PlanId { get; set; }
        public Guid CustomerId { get; set; }

        public ChangeSubscriptionModel ChangeSubscriptionModel { get; set; }

    }
}
