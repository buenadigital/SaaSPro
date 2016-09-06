using System;
using SaaSPro.Domain;

namespace SaaSPro.Services.Interfaces
{
    public interface IApiSessionTokenService
    {
        ApiSessionToken Add(User user, int timeout);
        ApiSessionToken Details(Guid id);
        ApiSessionToken Details(string token, Guid customerId);
        void UpdateExpirationDate(Guid id, int timeout);
        void UpdateQuestionAnswered(Guid id);
    }
}