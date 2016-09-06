using SaaSPro.Common.Helpers;
using SaaSPro.Domain;
using System;

namespace SaaSPro.Web
{
    public class CustomerInstance : IDisposable
    {
        public string Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public string Name { get; private set; }
        public string EncryptionKey { get; private set; }
        public Customer OriginalCustomer { get; private set; }

        public CustomerInstance(string id, Guid customerId, string customerName, string encryptionKey, Customer customer)
        {
            Ensure.Argument.NotNullOrEmpty(id, "id");
            Ensure.Argument.NotNullOrEmpty(customerName, "customerName");
            Ensure.Argument.NotNullOrEmpty(encryptionKey, "encryptionKey");
            Ensure.Argument.NotNull(customer, "customer");

            Id = id;
            CustomerId = customerId;
            Name = customerName;
            EncryptionKey = encryptionKey;
            OriginalCustomer = customer;
        }

        public void Start()
        {
            Events.Raise(new CustomerInstanceStartedEvent
            {
                InstanceId = Id
            });
        }

        internal static CustomerInstance Create(string instanceKey, Customer customer)
        {
            if (string.IsNullOrEmpty(instanceKey))
                throw new ArgumentNullException(nameof(instanceKey));

            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            return new CustomerInstance(instanceKey, customer.Id, customer.FullName, customer.EncryptionKey, customer);
        }

        public void Dispose()
        {
        }

        public override string ToString()
        {
            return $"{Name}:{Id}:{CustomerId}";
        }
    }
}