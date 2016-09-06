using System;
using System.Configuration;
using System.Web.Http;
using AutoMapper;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.Messaging.UserService;
using SaaSPro.Web.API.Attributes;
using SaaSPro.Web.API.Exceptions;
using SaaSPro.Web.API.Model.Auth;

namespace SaaSPro.Web.API.Controllers
{
    public class ApiAuthController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IApiSessionTokenService _apiSessionTokenService;

        public ApiAuthController(IUserService userService, IApiSessionTokenService apiSessionTokenService)
        {
            _userService = userService;
            _apiSessionTokenService = apiSessionTokenService;
        }

        [HttpPost]
        public ApiSessionTokenModel Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid) throw new ModelException(ModelState);

            GetUserRequest request = new GetUserRequest
            {
                Email = model.Email,
                CustomerId = Customer.Id
            };

            var user = _userService.GetUser(request);

            if (user == null)
            {
                throw new ApiException(ApiException.Errors.Auth.UserNotFound);
            }

            if (!user.ValidatePassword(model.Password))
            {
                throw new ApiException(ApiException.Errors.Auth.IncorrectPassword);
            }

            var setting = ConfigurationManager.AppSettings[Constants.ApiSessionTimeout];
            int timeout;
            if (!int.TryParse(setting, out timeout)) timeout = 20;

            var apiSessionToken = _apiSessionTokenService.Add(user, timeout);
            
            return Mapper.Map<ApiSessionTokenModel>(apiSessionToken);
        }

        [HttpPost]
        [ApiLoginAuthorize]
        public void ValidateSecurityAnswer(ValidateSecurityAnswerModel model)
        {
            if (!ModelState.IsValid) throw new ModelException(ModelState);

            var apiToken = _apiSessionTokenService.Details(Guid.Parse(User.Identity.Name));
            var user = apiToken.User;

            var question = user.GetSecurityQuestion(apiToken.SecurityQuestionId);

            if(!question.ValidateAnswer(model.Answer))
            {
                throw new ApiException(ApiException.Errors.Auth.IncorrectSecurityAnswer);
            }

            _apiSessionTokenService.UpdateQuestionAnswered(apiToken.Id);
        }
    }
}
