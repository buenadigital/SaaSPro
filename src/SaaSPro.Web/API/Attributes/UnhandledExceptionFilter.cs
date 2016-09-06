using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using SaaSPro.Web.API.Exceptions;
using SaaSPro.Web.API.Model.General;

namespace SaaSPro.Web.API.Attributes
{
    public class UnhandledExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            // todo log exception
            //LogHelper.LogException(context.Exception);

            var responseException = context.Exception is ApiException
                ? (ApiException) context.Exception
                : new ApiException(ApiException.Errors.General.GeneralError, innerException: context.Exception);

            context.Response =
                context.Request.CreateResponse(HttpStatusCode.OK,
                new ErrorResponse
                {
                    Success = false,
                    Error = responseException.Error
                });

            base.OnException(context);
        }
    }
}