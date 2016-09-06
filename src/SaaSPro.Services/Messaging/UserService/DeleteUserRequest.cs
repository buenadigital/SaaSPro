using System;

namespace SaaSPro.Services.Messaging.UserService
{
    public class DeleteUserRequest
    {
        public Guid Id { get; set; }
        public Guid CurrentUserID { get; set; }
   
    }
}
