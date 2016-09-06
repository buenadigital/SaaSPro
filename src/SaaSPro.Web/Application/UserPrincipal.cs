using SaaSPro.Domain;
using System;
using System.Security.Claims;

namespace SaaSPro.Web
{
    public class UserPrincipal : ClaimsPrincipal
    {
        public UserPrincipal(ClaimsPrincipal principal) : base(principal)
        {

        }

        public virtual Guid Id => Guid.Parse(FindFirst(ClaimTypes.PrimarySid).Value);

        public bool IsAdmin => IsInRole(DefaultRoles.Admin);

        public bool HasPasswordExpired => bool.Parse(FindFirst(ClaimTypes.Expiration).ValueOrDefault());
    }
}