using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using SaaSPro.Common.Helpers;

namespace SaaSPro.Common.Web
{
    public static class Extensions
    {
        /// <summary>
        /// Wraps the <typeparamref name="TResult"/> in an <see cref="AlertResult{TResult}"/> that inserts the alert message into TempData.
        /// </summary>
        public static AlertResult<TResult> AndAlert<TResult>(this TResult result, AlertType alertType, string title, string description = null) where TResult : ActionResult
        {
            Ensure.Argument.NotNullOrEmpty(title, "title");
            return new AlertResult<TResult>(result, alertType, title, description);
        }

        /// <summary>
        /// Renders an Alert if one exists in TempData (requires a partial view named _Alert)
        /// </summary>
        public static MvcHtmlString Alert(this HtmlHelper html)
        {
            var alert = html.ViewContext.TempData[typeof(Alert).FullName] as Alert;
            if (alert != null)
                return html.Partial("_Alert", alert);

            return MvcHtmlString.Empty;
        }

        /// <summary>
        /// A helper for performing conditional IF logic using Razor
        /// </summary>
        public static MvcHtmlString If(this HtmlHelper html, bool condition, string trueString)
        {
            Ensure.Argument.NotNull(html, "html");
            return html.IfElse(condition, h => MvcHtmlString.Create(trueString), h => MvcHtmlString.Empty);
        }

        /// <summary>
        /// A helper for performing conditional IF,ELSE logic using Razor
        /// </summary>
        public static MvcHtmlString IfElse(this HtmlHelper html, bool condition, Func<HtmlHelper, MvcHtmlString> trueAction, Func<HtmlHelper, MvcHtmlString> falseAction)
        {
            Ensure.Argument.NotNull(html, "html");
            Ensure.Argument.NotNull(trueAction, "trueAction");
            Ensure.Argument.NotNull(falseAction, "falseAction");
            return (condition ? trueAction : falseAction).Invoke(html);
        }
    }
}
