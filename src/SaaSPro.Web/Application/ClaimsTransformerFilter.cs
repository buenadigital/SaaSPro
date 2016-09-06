using SaaSPro.Common.Helpers;
using SaaSPro.Common;
using SaaSPro.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace SaaSPro.Web
{
    public class ClaimsTransformerFilter : IAuthorizationFilter
    {
        public Func<IRepository<User>> UserRepositoryFactory { get; set; }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var currentPrincipal = filterContext.HttpContext.User;

            if (currentPrincipal.Identity.IsAuthenticated)
            {
                var user = UserRepositoryFactory().Query()
                    .FirstOrDefault(u => u.Email == currentPrincipal.Identity.Name);

                if (user != null)
                {
                    TransformPrincipal(filterContext.HttpContext, user);
                }
            }
        }

        private void TransformPrincipal(HttpContextBase httpContext, User user)
        {
            var claims = new List<Claim>(new[] {
                new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Name, user.FullName), 
                new Claim(ClaimTypes.AuthenticationMethod, "Forms"),
                new Claim(ClaimTypes.Expiration, user.HasExpiredPassword.ToString()),
                new Claim(CustomClaimTypes.UserType, user.UserType.ToString())
            });

            user.Roles.ForEach(r => claims.Add(new Claim(ClaimTypes.Role, r.Name)));
            var identity = new ClaimsIdentity(claims, "Local");
            var principal = new ClaimsPrincipal(identity);

            httpContext.User = principal;
        }
    }
}