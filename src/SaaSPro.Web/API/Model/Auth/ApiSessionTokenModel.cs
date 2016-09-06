using System;

namespace SaaSPro.Web.API.Model.Auth
{
    public class ApiSessionTokenModel
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SecurityQuestion { get; set; }
    }
}