using System;
using System.Linq;
using SaaSPro.Services.Interfaces;
using SaaSPro.Data.Repositories;
using SaaSPro.Domain;


namespace SaaSPro.Services.Implementations
{
    public class ApiSessionTokenService : IApiSessionTokenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApiSessionTokenRepository _apiSessionTokenRepository;


        public ApiSessionTokenService(IUnitOfWork unitOfWork, IApiSessionTokenRepository apiSessionTokenRepository)
        {
            _unitOfWork = unitOfWork;
            _apiSessionTokenRepository = apiSessionTokenRepository;
        }
        
        public ApiSessionToken Add(User user, int timeout)
        {
            var apiToken = new ApiSessionToken(user, timeout);
            _apiSessionTokenRepository.Add(apiToken);
            _unitOfWork.Commit();

            return apiToken;
        }

        public ApiSessionToken Details(Guid  id)
        {
            return _apiSessionTokenRepository.Get(id);
        }

        public ApiSessionToken Details(string token, Guid customerId)
        {
            return _apiSessionTokenRepository.Query().FirstOrDefault(x => x.Token == token && x.User.CustomerId == customerId);
        }

        public void UpdateExpirationDate(Guid id, int timeout)
        {
            var apiToken = _apiSessionTokenRepository.Get(id);
            apiToken.UpdateExpirationDate(timeout);
            _unitOfWork.Commit();
        }

        public void UpdateQuestionAnswered(Guid id)
        {
            var apiToken = _apiSessionTokenRepository.Get(id);
            apiToken.UpdateQuestionAnswered();
            _unitOfWork.Commit();
        }
    }
}
