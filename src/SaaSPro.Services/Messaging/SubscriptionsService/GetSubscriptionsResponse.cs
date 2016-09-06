using SaaSPro.Services.ViewModels;

namespace SaaSPro.Services.Messaging.SubscriptionsService
{
    public class GetSubscriptionsResponse
    {
        public ChangeSubscriptionModel ChangeSubscriptionModel { get; set; }
        public SubscriptionModel SubscriptionModel { get; set; }
        public bool HasPlan { get; set; }

        public GetSubscriptionsResponse()
        {
            ChangeSubscriptionModel = new ChangeSubscriptionModel();
            SubscriptionModel = new SubscriptionModel();
        }
    }
}
