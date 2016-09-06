namespace SaaSPro.Services.Messaging.CustomerService
{
    public class SetupPaymentPlanReponse: BaseResponse
    {
        public bool HasPlan { get; set; }
        public string Status { get; set; }
        public bool IsStatusActive { get; set; }
    }
}
