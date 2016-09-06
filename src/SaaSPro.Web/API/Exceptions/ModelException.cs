using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.ModelBinding;

namespace SaaSPro.Web.API.Exceptions
{
    public class ModelException : ApiException
    {
        public ModelException(ModelStateDictionary modelState, string message = null, Exception innerException = null)
            : base(message ?? Errors.General.ModelError.ErrorMessage, innerException)
        {
            var modelErrors = new List<ResponseModelError.ModelError>();

            foreach (var state in modelState)
            {
                var errors = new List<string>();

                foreach (var error in state.Value.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }

                if (errors.Any())
                {
                    modelErrors.Add(new ResponseModelError.ModelError
                    {
                        Field = GetCorrectKey(state.Key),
                        Errors = errors
                    });
                }
            }

            Error = new ResponseModelError
            {
                ErrorCode = Errors.General.ModelError.ErrorCode,
                ErrorMessage = Errors.General.ModelError.ErrorMessage,
                ModelErrors = modelErrors
            };
        }

        private static string GetCorrectKey(string source)
        {
            var dotIndex = source.IndexOf(".");
            return dotIndex < 0 ? source : source.Substring(dotIndex + 1);
        }

        public class ResponseModelError : ApiException.ApiError
        {
            public class ModelError
            {
                public string Field { get; set; }
                public IEnumerable<string> Errors { get; set; }
            }

            public IEnumerable<ModelError> ModelErrors { get; set; }
        }
    }
}