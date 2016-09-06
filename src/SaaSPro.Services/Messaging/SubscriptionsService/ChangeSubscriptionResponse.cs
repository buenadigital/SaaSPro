namespace SaaSPro.Services.Messaging.SubscriptionsService
{
    public class ChangeSubscriptionResponse:BaseResponse
    {
        public bool IsStatusActive { get; set; }
        public string Result { get; set; }
    }
}
