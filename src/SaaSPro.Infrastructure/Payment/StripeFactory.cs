namespace SaaSPro.Infrastructure.Payment
{
    public class StripeFactory
    {
        private static IStripeService _stripeService;

        public static void InitializeStripeFactory(IStripeService stripeService)
        {
            _stripeService = stripeService;
        }

        public static IStripeService GetStripeService()
        {
            return _stripeService;
        }
    }
}
