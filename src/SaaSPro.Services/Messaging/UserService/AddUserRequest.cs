using System;
using SaaSPro.Services.ViewModels;
using SaaSPro.Domain;

namespace SaaSPro.Services.Messaging.UserService
{
    public class AddUserRequest
    {
        public UsersAddModel Model { get; set; }
        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }

    }
}
