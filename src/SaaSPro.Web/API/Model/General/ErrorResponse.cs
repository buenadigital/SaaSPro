using SaaSPro.Web.API.Exceptions;

namespace SaaSPro.Web.API.Model.General
{
    public class ErrorResponse : ApiResponse
    {
        public ApiException.ApiError Error { get; set; }

        public ErrorResponse()
        {
            Success = false;
        }
    }
}