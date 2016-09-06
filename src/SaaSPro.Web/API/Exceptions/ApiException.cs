using System;

namespace SaaSPro.Web.API.Exceptions
{
    public class ApiException : Exception
    {
        public ApiError Error { get; protected set; }

        public override string Message => string.IsNullOrEmpty(base.Message) ? Error.ErrorMessage : base.Message;

        public ApiException(ApiError error, string message = "", Exception innerException = null)
            : base(message, innerException)
        {
            Error = error;
        }

        protected ApiException(string message = "", Exception innerException = null)
            : base(message, innerException)
        {
            Error = null;
        }

        public class ApiError
        {
            public string ErrorCode { get; set; }
            public string ErrorMessage { get; set; }
        }

        public static class Errors
        {
            public static class General
            {
                public static readonly ApiError GeneralError = new ApiError { ErrorCode = "GN001", ErrorMessage = "General error. Please contact support." };
                public static readonly ApiError ModelError = new ApiError { ErrorCode = "GN002", ErrorMessage = "Incorrect input values found." };
                public static readonly ApiError NotAuthorized = new ApiError { ErrorCode = "GN003", ErrorMessage = "Client not authorized." };
            }

            public static class Auth
            {
                public static readonly ApiError SessionTokenExpired = new ApiError { ErrorCode = "AU001", ErrorMessage = "Session token is expired." };
                public static readonly ApiError QuestionNotAnswered = new ApiError { ErrorCode = "AU002", ErrorMessage = "Security question is not answered." };
                public static readonly ApiError UserNotFound = new ApiError { ErrorCode = "AU003", ErrorMessage = "User is not found." };
                public static readonly ApiError IncorrectPassword = new ApiError { ErrorCode = "AU004", ErrorMessage = "Incorrect password." };
                public static readonly ApiError IncorrectSecurityAnswer = new ApiError { ErrorCode = "AU005", ErrorMessage = "Provided answer is incorrect." };
            }
        }
    }
}