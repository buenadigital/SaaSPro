using System;
using SaaSPro.Services.ViewModels;

namespace SaaSPro.Services.Messaging.UserService
{
    public class UpdateSecurityQuestionsRequest
    {
        public UsersUpdateSecurityQuestionsModel UsersUpdateSecurityQuestionsModel { get; set; }
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
    }
}
