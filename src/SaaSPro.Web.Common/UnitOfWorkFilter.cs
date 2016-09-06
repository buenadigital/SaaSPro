using System;
using System.Web.Mvc;
using SaaSPro.Data.Repositories;

namespace SaaSPro.Web.Common
{
    public class UnitOfWorkFilter : IActionFilter
    {
        public Func<IUnitOfWork> UnitOfWorkFactory { get; set; }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var uow = UnitOfWorkFactory();
         //   uow.Begin();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var uow = UnitOfWorkFactory();

            if (filterContext.Exception == null || filterContext.ExceptionHandled)
            {
           //     uow.End();
            }
        }
    }
}