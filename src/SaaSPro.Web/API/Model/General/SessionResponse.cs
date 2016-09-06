using System;

namespace SaaSPro.Web.API.Model.General
{
    public class SessionResponse : ApiResponse
    {
        public DateTime ExpirationDate { get; set; }
    }
}