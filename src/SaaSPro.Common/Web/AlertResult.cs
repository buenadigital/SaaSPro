using System.Web.Mvc;
using SaaSPro.Common.Helpers;

namespace SaaSPro.Common.Web
{
    public class AlertResult<TResult> : ActionResult where TResult : ActionResult
    {
        public TResult Result { get; private set; }
        public Alert Message { get; private set; }

        public AlertResult(TResult result, AlertType alertType, string title, string description = null)
        {
            Ensure.Argument.NotNull(result, nameof(result));
            Ensure.Argument.NotNullOrEmpty(title, nameof(title));

            Result = result;
            Message = new Alert(alertType, title, description);
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.Controller.TempData[typeof(Alert).FullName] = Message;
            Result.ExecuteResult(context);
        }
    }
}
