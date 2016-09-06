using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Stripe;

namespace SaaSPro.Infrastructure.Payment
{
   public class StripeAdapter:IStripeService
    {
        public IEnumerable<StripePlan> GetAvailablePlans()
        {
            var planService = new StripePlanService();
            return planService.List();
        }

        public StripeCustomer GetCustomer(string id)
        {
            var customerService = new StripeCustomerService();
            return customerService.Get(id);
        }

        public StripePlan GetCustomerPlan(string customerId)
        {
            var customerService = new StripeCustomerService();
            var customer = customerService.Get(customerId);

            if (customer.StripeSubscriptionList != null)
                return customer.StripeSubscriptionList.Data.Any()
                    ? customer.StripeSubscriptionList.Data.First().StripePlan
                    : null;
            else
                return null;
        }

        public IEnumerable<StripeCharge> GetCustomerPayments(int limit, string customerId)
        {

            var chargeService = new StripeChargeService();
            return chargeService.List(new StripeChargeListOptions
            {
                Limit = limit,
                CustomerId = customerId
            });
        }

        public StripeCharge GetCustomerPayment(string paymentId)
        {
            var chargeService = new StripeChargeService();
            return chargeService.Get(paymentId);
        }

        public StripeCustomer CreateCustomer(string name, string email)
        {
            var newCustomer = new StripeCustomerCreateOptions
            {
                Email = email,
                Description = $"{name} ({email})"
            };

            var customerService = new StripeCustomerService();
			var stripeCustomer = customerService.Create(newCustomer);
			return stripeCustomer;
        }

        public string AssignCustomerPlan(string customerId, string planId, string cardNumber,
                                       string cardCvc, int cardExpirationMonth, int cardExpirationYear)
        {

            // Create token
            var token = CreateToken(cardNumber, cardCvc, cardExpirationMonth, cardExpirationYear);

            var subscriptionService = new StripeSubscriptionService();
            var subscription = subscriptionService.List(customerId).FirstOrDefault();
            if (subscription == null)
            {
                var options = new StripeSubscriptionCreateOptions
                {
                    Card = new StripeCreditCardOptions
                    {
                        TokenId = token.Id
                    },
                    PlanId = planId
                };

                subscription = subscriptionService.Create(customerId, planId, options);
                    
            }
            else
            {
                var options = new StripeSubscriptionUpdateOptions
                {
                    Card = new StripeCreditCardOptions
                    {
                        TokenId = token.Id
                    },
                    PlanId = planId
                };

                subscription = subscriptionService.Update(customerId, subscription.Id, options);
            }


            return subscription.Status;
        }

        private static StripeToken CreateToken(string cardNumber, string cardCvc, int cardExpMonth,
                                        int cardExpYear)
        {
            var myToken = new StripeTokenCreateOptions
            {
                Card = new StripeCreditCardOptions
                {
                    Cvc = cardCvc,
                    ExpirationMonth = cardExpMonth.ToString(CultureInfo.InvariantCulture),
                    ExpirationYear = cardExpYear.ToString(CultureInfo.InvariantCulture),
                    Number = cardNumber
                }
            };

            var tokenService = new StripeTokenService();
            var stripeToken = tokenService.Create(myToken);
            return stripeToken;
        }

        public void DeleteCustomer(string customerId)
        {

            var customerService = new StripeCustomerService();
            var customer = GetCustomer(customerId);
            if (string.IsNullOrWhiteSpace(customer.Email))
                customerService.Delete(customerId);
        }

        public string CloseCustomerPlan(string customerId)
        {
            var subscriptionService = new StripeSubscriptionService();
            var subscription = subscriptionService.List(customerId).FirstOrDefault();
            if (subscription == null) throw new NullReferenceException("Subscription for stripe customer is not found");
            subscription = subscriptionService.Cancel(customerId, subscription.Id);
            return subscription.Status;
        }

        public void Refund(string paymentId, int amount)
        {
            var chargeService = new StripeRefundService();
            new StripeRefundCreateOptions().Amount = amount;
            chargeService.Create(paymentId); 
        }
    }
}
