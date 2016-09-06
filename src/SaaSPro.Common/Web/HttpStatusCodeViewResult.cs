using System.Net;
using System.Web.Mvc;

namespace SaaSPro.Common.Web
{
    /// <summary>
    /// An action result that returns a View for a specific HTTP status code.
    /// </summary>
    public class HttpStatusCodeViewResult : ViewResult
    {
        private readonly HttpStatusCode _statusCode;
        private readonly string _description;

        public HttpStatusCodeViewResult(HttpStatusCode statusCode, string description = null) :
            this(null, statusCode, description) { }

        public HttpStatusCodeViewResult(string viewName, HttpStatusCode statusCode, string description = null)
        {
            _statusCode = statusCode;
            _description = description;
            ViewName = viewName;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var httpContext = context.HttpContext;
            var response = httpContext.Response;

            response.TrySkipIisCustomErrors = true;
            response.StatusCode = (int)_statusCode;
            response.StatusDescription = _description;

            base.ExecuteResult(context);
        }
    }
}
