using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StructureMap;
using System.Web.Http;
using SaaSPro.Web.API.AuthHandlers;

namespace SaaSPro.Web
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
            // Handlers
            config.MessageHandlers.Add(ObjectFactory.GetInstance<CustomerAuthHandler>());
            config.MessageHandlers.Add(ObjectFactory.GetInstance<ApiAuthHandler>());
            
            // Filters
            //config.Filters.Add(new ApiAuthorizeAttribute());
            
            // Formatters
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };

            //// Routes
            //config.Routes.MapHttpRoute(
            //    name: "ReferralDocuments",
            //    routeTemplate: "api/referrals/{referralId}/documents/{id}",
            //    defaults: new { controller = "referraldocuments", id = RouteParameter.Optional }
            //);

            //config.Routes.MapHttpRoute(
            //    name: "Audit",
            //    routeTemplate: "api/auditlog/{entityType}/{id}",
            //    defaults: new { controller = "auditlog" }
            //);

            // auth
            config.Routes.MapHttpRoute("ApiAuth_Login", "api/auth/login", new { controller = "ApiAuth", action = "Login" });
            config.Routes.MapHttpRoute("ApiAuth_ValidateSecurityAnswer", "api/auth/validate-security-answer", new { controller = "ApiAuth", action = "ValidateSecurityAnswer" });

            //// auth
            //config.Routes.MapHttpRoute("ApiAuditLog_GetAll", "api/audit-log/get-all", new { controller = "ApiAuditLog", action = "GetAll" });

            config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
	}
}
