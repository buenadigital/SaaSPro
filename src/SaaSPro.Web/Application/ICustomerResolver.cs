using SaaSPro.Domain;
using System;

namespace SaaSPro.Web
{
    public interface ICustomerResolver
    {
        string CreateInstanceKey(Uri requestUri);
        Customer ResolveCustomer(string instanceKey);
    }
}