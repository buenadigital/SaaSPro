using System;

namespace SaaSPro.Web
{
    public interface ICustomerHost
    {
        CustomerInstance GetOrStartCustomerInstance(Uri requestUri);
    }
}
