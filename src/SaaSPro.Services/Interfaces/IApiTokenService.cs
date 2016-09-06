using System;
using SaaSPro.Services.ViewModels;
using SaaSPro.Domain;

namespace SaaSPro.Services.Interfaces
{
    public interface IApiTokenService
    {
        ApiTokensListModel List(Guid customerId);
        void Add(ApiTokensAddModel model, Customer customer);
        ApiTokensUpdateModel Details(Guid id);
        ApiToken Details(string token, Guid customerId);
        void Update(ApiTokensUpdateModel model, Guid id);
        void Delete(Guid id);
    }
}