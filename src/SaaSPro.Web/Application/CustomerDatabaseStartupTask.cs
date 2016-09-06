using SaaSPro.Common.Helpers;
using StructureMap;
using System.Diagnostics;

namespace SaaSPro.Web
{
    public class CustomerDatabaseStartupTask : ICustomerStartupTask
    {
        private readonly IContainer _container;

        public CustomerDatabaseStartupTask(IContainer container)
        {
            Ensure.Argument.NotNull(container, "container");
            _container = container;
        }
        
        public void Execute(CustomerInstance customer)
        {
            Trace.WriteLine($"Customer {customer.Name}: Initializing EF.");
        }
    }
}