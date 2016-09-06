using System;

namespace SaaSPro.Services.Messaging.UserService
{
    public class GetUserRequest
    {
        public string Email { get; set; }
        public Guid CustomerId { get; set; }
        public string GetBy { get; set; }

    }
}
