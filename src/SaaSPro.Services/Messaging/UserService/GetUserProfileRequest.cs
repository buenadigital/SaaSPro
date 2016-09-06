using System;
using SaaSPro.Domain;

namespace SaaSPro.Services.Messaging.UserService
{
    public class GetUserProfileRequest
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public UserType UserType { get; set; }
    }
}
