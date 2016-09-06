using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using SaaSPro.Domain;
using SaaSPro.Services.Interfaces;
using SaaSPro.Web.API.Model.General;
using StructureMap;

namespace SaaSPro.Web.API.Attributes
{
    public class ApiResponseAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);

            if (actionExecutedContext.Exception != null) return;

            ApiSessionToken sessionToken = null;

            var identity = HttpContext.Current.User?.Identity;
            if (identity?.AuthenticationType != null && identity.AuthenticationType == Constants.ApiSessionKeySchemeName)
            {
                var apiSessionTokenService = ObjectFactory.Container.GetInstance<IApiSessionTokenService>();
                sessionToken = apiSessionTokenService.Details(Guid.Parse(identity.Name));
            }

            ApiResponse response;

            if (sessionToken != null)
            {
                response = actionExecutedContext.Response.Content == null
                    ? new SessionResponse
                    {
                        Success = true,
                        ExpirationDate = sessionToken.ExpirationDate
                    }
                    : new SessionDataResponse
                    {
                        Success = true,
                        Data = ((ObjectContent)actionExecutedContext.Response.Content).Value,
                        ExpirationDate = sessionToken.ExpirationDate
                    };
            }
            else
            {
                response = actionExecutedContext.Response.Content == null
                    ? new NonSessionResponse
                    {
                        Success = true
                    }
                    : new NonSessionDataResponse
                    {
                        Success = true,
                        Data = ((ObjectContent)actionExecutedContext.Response.Content).Value
                    };
            }

            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}