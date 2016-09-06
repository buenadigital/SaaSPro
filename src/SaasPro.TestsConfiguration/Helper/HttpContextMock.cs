using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Principal;
using System.Web;
using Moq;

namespace SaasPro.TestsConfiguration.Helper
{
	public class HttpContextMock
	{
		public HttpContextMock(IPrincipal user = null, bool isAjaxRequest = false,
			IEnumerable<KeyValuePair<string, string>> requestParams = null)
		{
			Server = new Mock<HttpServerUtilityBase>(MockBehavior.Loose);

			Response = new Mock<HttpResponseBase>(MockBehavior.Strict);
			Response.Setup(res => res.ApplyAppPathModifier(It.IsAny<string>())).Returns((string virtualPath) => virtualPath);
			Response.Setup(res => res.Cookies).Returns(new HttpCookieCollection());

			Request = new Mock<HttpRequestBase>(MockBehavior.Strict);
			Request.Setup(req => req.UserHostAddress).Returns("127.0.0.1");
			Request.Setup(req => req.ApplicationPath).Returns("~/");
			Request.Setup(req => req.AppRelativeCurrentExecutionFilePath).Returns("~/");
			Request.Setup(req => req.PathInfo).Returns(string.Empty);
			Request.Setup(req => req.Cookies).Returns(new HttpCookieCollection());
			Request.Setup(req => req.QueryString).Returns(new NameValueCollection());
			Request.Setup(req => req.Form).Returns(new NameValueCollection());

			if (isAjaxRequest)
				AddRequestParameter("X-Requested-With", "XMLHttpRequest");

			if (requestParams != null)
				AddRequestParameters(requestParams);

			Session = new HttpSessionMock();
			Items = new Dictionary<object, object>();

			Context = new Mock<HttpContextBase>();
			Context.SetupGet(c => c.Request).Returns(Request.Object);
			Context.SetupGet(c => c.Response).Returns(Response.Object);
			Context.SetupGet(c => c.Server).Returns(Server.Object);
			Context.SetupGet(c => c.Session).Returns(Session);
			Context.SetupGet(c => c.Items).Returns(Items);

			if (user == null) return;

			Context.SetupGet(x => x.User).Returns(user);
			Context.SetupGet(x => x.Request.IsAuthenticated)
				.Returns(user.Identity.IsAuthenticated);
		}

		public HttpContextBase HttpContext => Context.Object;

	    public Mock<HttpContextBase> Context { get; set; }
		public Mock<HttpServerUtilityBase> Server { get; set; }
		public Mock<HttpResponseBase> Response { get; set; }
		public Mock<HttpRequestBase> Request { get; set; }
		public HttpSessionMock Session { get; set; }
		public Dictionary<object, object> Items { get; set; }

		public void AddRequestParameters(IEnumerable<KeyValuePair<string, string>> parameters)
		{
			foreach (var param in parameters)
			{
				AddRequestParameter(param.Key, param.Value);
			}
		}

		public void AddRequestParameter(string name, string value)
		{
			Request.Setup(r => r[name]).Returns(value);
		}
	}

	public class HttpSessionMock : HttpSessionStateBase
	{
		private readonly Dictionary<string, object> _objects = new Dictionary<string, object>();

		public override object this[string name]
		{
			get { return (_objects.ContainsKey(name)) ? _objects[name] : null; }
			set { _objects[name] = value; }
		}

		public override void Add(string name, object value)
		{
			_objects.Add(name, value);
			base.Add(name, value);
		}

		public override void Remove(string name)
		{
			_objects.Remove(name);
			base.Remove(name);
		}

		public override void Clear()
		{
			_objects.Clear();
			base.Clear();
		}

		public override void RemoveAll()
		{
			Clear();
		}
	}
}
