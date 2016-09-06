using System;

namespace SaaSPro.Services.Messaging.UserService
{
    public class GetSecurityQuestionsRequest
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }
    }
}
