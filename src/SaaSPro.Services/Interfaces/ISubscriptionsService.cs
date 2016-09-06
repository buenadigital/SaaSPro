using System;
using SaaSPro.Services.Messaging.SubscriptionsService;

namespace SaaSPro.Services.Interfaces
{
    public interface ISubscriptionsService
    {
        GetSubscriptionsResponse List(Guid customerId);
        CancelSubscriptionResponse CancelSubscription(Guid customerId);
        ChangeSubscriptionResponse ChangeSubscription(ChangeSubscriptionRequest request);
    }
}