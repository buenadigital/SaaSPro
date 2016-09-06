using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.SessionState;
using SaaSPro.Domain;
using SaaSPro.Web;

namespace SaasPro.TestsConfiguration.Helper
{
	public class TestsHttpContextInitialize
	{
		private readonly Uri _siteUri;
		private readonly User _currentUser;
		private readonly ICustomerHost _customerHost;

		public TestsHttpContextInitialize(Uri siteUri, User currentUser, ICustomerHost customerHost)
		{
			_siteUri = siteUri;
			_currentUser = currentUser;
			_customerHost = customerHost;
		}

		private HttpBrowserCapabilities GetHttpBrowser()
		{
			return new HttpBrowserCapabilities
			{
				Capabilities = new Dictionary<string, string>
				{
					{"supportsEmptyStringInCookieValue", "false"},
					{"cookies", "false"},
					{"browser", "Firefox" },
					{"canSendMail", "false" }
				}
			};
		}

		private CustomerInstance GetCustomerInstance()
		{
			return _customerHost.GetOrStartCustomerInstance(_siteUri);
		}


		public HttpContextBase GetTestsHttpContextBase()
		{
			var httpContext = new HttpContextMock();
			httpContext.Request.Setup(t => t.Url).Returns(_siteUri);
			httpContext.HttpContext.ApplicationInstance = new HttpApplication();
			httpContext.Request.Setup(r => r.Cookies).Returns(new HttpCookieCollection());
			httpContext.Request.Setup(r => r.Form).Returns(new NameValueCollection());
			httpContext.Request.Setup(q => q.QueryString).Returns(new NameValueCollection());
			httpContext.Request.Setup(q => q.IsSecureConnection).Returns(false);
			httpContext.Request.Setup(q => q.Browser).Returns(new HttpBrowserCapabilitiesWrapper(GetHttpBrowser()));
			httpContext.Response.Setup(r => r.Cookies).Returns(new HttpCookieCollection());

			var identity = new ClaimsIdentity();
			identity.AddClaim(new Claim(ClaimTypes.PrimarySid, _currentUser.Id.ToString()));
			httpContext.Context.Setup(x => x.User).Returns(new ClaimsPrincipal(identity));

			httpContext.Items[Constants.CurrentCustomerInstanceKey] = GetCustomerInstance();
			

			return httpContext.HttpContext;
		}

		public HttpContext GetTestHttpContext()
		{
			var httpRequest = new HttpRequest("", _siteUri.AbsoluteUri, "");
			var stringWriter = new StringWriter();
			var httpResponse = new HttpResponse(stringWriter);
            httpRequest.Browser = GetHttpBrowser();

            var httpContext = new HttpContext(httpRequest, httpResponse);

			var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
				new HttpStaticObjectsCollection(), 10, true,
				HttpCookieMode.AutoDetect,
				SessionStateMode.InProc, false);

			httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(
				BindingFlags.NonPublic | BindingFlags.Instance,
				null, CallingConventions.Standard,
				new[] { typeof(HttpSessionStateContainer) },
				null)
				.Invoke(new object[] { sessionContainer });

			httpContext.Items[Constants.CurrentCustomerInstanceKey] = GetCustomerInstance();

			//HttpContext.Current = httpContext;
			return httpContext;
		}

        public HttpContext GetTestApiHttpContext(ApiSessionToken apiToken)
        {
            var httpRequest = new HttpRequest("", _siteUri.AbsoluteUri, "");
            var stringWriter = new StringWriter();
            var httpResponse = new HttpResponse(stringWriter);
            httpRequest.Browser = GetHttpBrowser();

            var httpContext = new HttpContext(httpRequest, httpResponse);
            httpContext.User = new GenericPrincipal(new GenericIdentity(apiToken.Id.ToString(), Constants.ApiSessionKeySchemeName), null);
            httpContext.Items[Constants.CurrentCustomerInstanceKey] = GetCustomerInstance();
            
            return httpContext;
        }
    }
}
