using System;
using System.Configuration;
using System.Web.Http;
using System.Web.Http.Controllers;
using SaaSPro.Services.Interfaces;
using SaaSPro.Web.API.Exceptions;
using StructureMap;

namespace SaaSPro.Web.API.Attributes
{
    public class ApiLoginAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var isAuthorized = base.IsAuthorized(actionContext);
            var user = actionContext.ControllerContext.RequestContext.Principal;

            // check Session Token authorization
            if (isAuthorized && user.Identity.AuthenticationType == Constants.ApiSessionKeySchemeName)
            {
                var apiSessionTokenService = ObjectFactory.GetInstance<IApiSessionTokenService>();
                var apiToken = apiSessionTokenService.Details(Guid.Parse(user.Identity.Name));
                
                if (apiToken.ExpirationDate < DateTime.Now)
                {
                    throw new ApiException(ApiException.Errors.Auth.SessionTokenExpired);
                }

                // update expiration date
                var sessionTimeout = int.Parse(ConfigurationManager.AppSettings[Constants.ApiSessionTimeout]);
                apiSessionTokenService.UpdateExpirationDate(apiToken.Id, sessionTimeout);
                return true;
            }

            throw new ApiException(ApiException.Errors.General.NotAuthorized);
        }
    }
}