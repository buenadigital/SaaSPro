using System;
using System.Collections.Generic;
using System.Linq;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.ViewModels;
using SaaSPro.Common;
using SaaSPro.Data.Repositories;
using SaaSPro.Domain;
using AutoMapper;


namespace SaaSPro.Services.Implementations
{
    public class ApiTokenService : IApiTokenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IApiTokenRepository _apiTokenRepository;


        public ApiTokenService(IUnitOfWork unitOfWork,IApiTokenRepository apiTokenRepository)
        {
            _unitOfWork = unitOfWork;
            _apiTokenRepository = apiTokenRepository;
        }

        public ApiTokensListModel List(Guid customerId)
        {
            var tokens = _apiTokenRepository.Fetch(q => q.OrderBy(t => t.Name)).Where(t => t.CustomerId == customerId);

            var model = new ApiTokensListModel
            {
                Tokens = Mapper.Map<IEnumerable<ApiTokensListModel.ApiTokenSummary>>(tokens)
            };

            return model;
        }

        public void Add(ApiTokensAddModel model,Customer customer)
        {
            var apiToken = new ApiToken(model.Name, customer);
            _apiTokenRepository.Add(apiToken);
            _unitOfWork.Commit();
        }

        public ApiTokensUpdateModel Details(Guid  id)
        {
            ApiToken apiToken = _apiTokenRepository.Get(id);

            ApiTokensUpdateModel model = Mapper.Map<ApiTokensUpdateModel>(apiToken);

            return model;
        }

        public ApiToken Details(string token, Guid customerId)
        {
           return _apiTokenRepository.Query().FirstOrDefault(x => x.Token == token && x.CustomerId == customerId);
        }

        public void Update(ApiTokensUpdateModel model, Guid id)
        {
            ApiToken apiToken = _apiTokenRepository.Get(id);

            apiToken.Name = model.Name;
            _apiTokenRepository.Update(apiToken);
            _unitOfWork.Commit();
        }
        public void Delete(Guid id)
        {
            ApiToken apiToken = _apiTokenRepository.Get(id);

            _apiTokenRepository.Delete(apiToken);

            _unitOfWork.Commit();
        }
    }
}
