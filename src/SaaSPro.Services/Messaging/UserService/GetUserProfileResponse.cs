using SaaSPro.Services.ViewModels;

namespace SaaSPro.Services.Messaging.UserService
{
    public class GetUserProfileResponse : BaseResponse
    {
        public UsersUpdateModel UsersUpdateModel { get; set; }

        public GetUserProfileResponse()
        {
            UsersUpdateModel=new UsersUpdateModel();
        }
    }
}
