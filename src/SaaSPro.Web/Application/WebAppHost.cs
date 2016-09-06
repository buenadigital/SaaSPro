using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using SaaSPro.Common.Helpers;

namespace SaaSPro.Web
{
    public class WebAppHost : ICustomerHost
    {
        private readonly ConcurrentDictionary<string, CustomerInstance> _customers
            = new ConcurrentDictionary<string, CustomerInstance>();

        private readonly ICustomerResolver _customerResolver;
        private readonly IEnumerable<ICustomerStartupTask> _startupTasks;

        public WebAppHost(ICustomerResolver customerResolver, IEnumerable<ICustomerStartupTask> startupTasks = null)
        {
            Ensure.Argument.NotNull(customerResolver, nameof(customerResolver));
            _customerResolver = customerResolver;
            _startupTasks = startupTasks;
        }

        public CustomerInstance GetOrStartCustomerInstance(Uri requestUri)
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            var instanceKey = _customerResolver.CreateInstanceKey(requestUri);

            CustomerInstance customerInstance;
            if (!_customers.TryGetValue(instanceKey, out customerInstance))
            {
                var customer = _customerResolver.ResolveCustomer(instanceKey);

                if (customer != null && customer.Enabled)
                {
                    customerInstance = CustomerInstance.Create(instanceKey, customer);
                    _customers.TryAdd(instanceKey, customerInstance);
                    Start(customerInstance);
                }
            }

            return customerInstance;
        }

        private void Start(CustomerInstance instance)
        {
            _startupTasks?.ForEach(t => t.Execute(instance));

            instance.Start();
        }
    }
}