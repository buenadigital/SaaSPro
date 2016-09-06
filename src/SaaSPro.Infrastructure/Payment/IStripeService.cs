using System.Collections.Generic;
using Stripe;

namespace SaaSPro.Infrastructure.Payment
{
    public interface IStripeService
    {
        IEnumerable<StripePlan> GetAvailablePlans();

        StripeCustomer GetCustomer(string id);
        StripePlan GetCustomerPlan(string customerId);
        IEnumerable<StripeCharge> GetCustomerPayments(int limit, string customerId);
        StripeCharge GetCustomerPayment(string paymentId);

        StripeCustomer CreateCustomer(string name, string email);
        string AssignCustomerPlan(string customerId, string planId, string cardNumber,
            string cardCvc, int cardExpirationMonth, int cardExpirationYear);

        void DeleteCustomer(string customerId);
        string CloseCustomerPlan(string customerId);

        /// <summary>
        /// Refund payment
        /// </summary>
        /// <param name="paymentId">Payment identifier</param>
        /// <param name="amount">Amount is a positive integer in cents</param>
        void Refund(string paymentId, int amount);
    }
}
