using System;
using System.Configuration;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SaaSPro.Web
{
    public class CustomerHttpModule : IHttpModule
    {
        public string NotFoundUri { get; set; }

        public CustomerHttpModule()
        {
            NotFoundUri = ConfigurationManager.AppSettings[Constants.CustomerNotFoundUrlSettingKey];
        }

        public void Init(HttpApplication application)
        {
            application.BeginRequest += (sender, e) =>  
                ValidateRequest(new HttpContextWrapper(((HttpApplication)sender).Context), DependencyResolver.Current.GetService<ICustomerHost>());
        }

        public virtual void ValidateRequest(HttpContextBase httpContext, ICustomerHost host)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var customerInstance = host.GetOrStartCustomerInstance(httpContext.Request.Url);

            if (customerInstance == null)
            {
                CustomerNotFound(httpContext.Response);
            }
            else
            {
                // Make the Customer instance available for the request
                httpContext.Items[Constants.CurrentCustomerInstanceKey] = customerInstance;
            }
        }

        protected virtual void CustomerNotFound(HttpResponseBase response)
        {
            response.StatusCode = (int)HttpStatusCode.SeeOther;
            response.Status = "303 Customer Not Found";
            response.RedirectLocation = NotFoundUri;
            response.SuppressContent = true;
            response.End();
        }

        public void Dispose()
        {

        }
    }
}