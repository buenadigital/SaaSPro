using System;
using SaaSPro.Common.Helpers;

namespace SaaSPro.Domain
{
    public class CustomerPaymentRefund  : Entity
    {
        public Guid CustomerId { get; protected set; }
        public string TransactionId { get; protected set; }
        public string ChargeId { get; protected set; } 
        public decimal Amount { get; protected set; }
        public DateTime Created   { get; set;}

        public virtual Customer Customer { get; protected set; }

        public CustomerPaymentRefund(Guid customerId, string transactionId, string chargeId, decimal amount, DateTime created)
        {
            Update(customerId, transactionId, chargeId, amount, created);
        }

        protected CustomerPaymentRefund() { }

        public void Update(Guid customerId, string transactionId, string chargeId, decimal amount, DateTime created)
        {
            Ensure.Argument.NotNullOrEmpty(transactionId, "transactionId");
            Ensure.Argument.NotNullOrEmpty(chargeId, "chargeId");

            CustomerId = customerId;
            TransactionId = transactionId;
            ChargeId = chargeId;
            Amount = amount;
            Created = created;
        }
    }
}