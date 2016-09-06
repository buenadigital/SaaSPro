using System.Security.Claims;

namespace SaaSPro.Web
{
    public static class ClaimExtensions
    {
        public static string ValueOrDefault(this Claim claim, string defaultValue = null)
        {
            if (claim == null)
                return defaultValue;

            return claim.Value;
        }
    }
}