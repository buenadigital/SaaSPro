using Stripe;

namespace SaaSPro.Services.Interfaces
{
    public  interface IStripeWebhookService
    {
        void ProcessEvent(string customerId, StripeEvent stripeEvent);
        bool ValidateCustomer(string customerId);
    }
}
