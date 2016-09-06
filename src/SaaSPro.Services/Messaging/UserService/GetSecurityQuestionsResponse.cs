using SaaSPro.Services.ViewModels;

namespace SaaSPro.Services.Messaging.UserService
{
    public class GetSecurityQuestionsResponse
    {
        public UsersUpdateSecurityQuestionsModel UsersUpdateSecurityQuestionsModel { get; set; }

        public GetSecurityQuestionsResponse()
        {
            UsersUpdateSecurityQuestionsModel = new UsersUpdateSecurityQuestionsModel();
        }
    }
}
