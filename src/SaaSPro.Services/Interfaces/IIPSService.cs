using System;
using SaaSPro.Domain;
using SaaSPro.Services.ViewModels;

namespace SaaSPro.Services.Interfaces
{
    public interface IIPSService
    {
        IPSListModel List(Guid customerId);
        void Add(IPSAddModel model, Customer customer);
        void Delete(Guid id);
    }
}
