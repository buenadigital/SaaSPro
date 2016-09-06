using System;
using SaaSPro.Services.Messaging.UserService;
using SaaSPro.Services.ViewModels;
using SaaSPro.Domain;

namespace SaaSPro.Services.Interfaces
{
    public interface IUserService
    {
        User GetUser(GetUserRequest request);
        User GetLoginSessionUser(Guid? userId);
        UsersListModel List(PagingCommand command);
        DeleteUserResponse Delete(DeleteUserRequest request);
        AddUserResponse Add(AddUserRequest request);
        void SetPassword(Guid userId, string newPassword, bool expireImmediately = false);
        UpdateProfileResponse UpdateProfile(UpdateProfileRequest model);
        GetUserProfileResponse GetUserProfile(GetUserProfileRequest request);
        UsersAddModel AddUserModel(Guid customerId, UserType userType);
        GetSecurityQuestionsResponse GetSecurityQuestions(GetSecurityQuestionsRequest model);
        UpdateSecurityQuestionsResponse UpdateSecurityQuestions(UpdateSecurityQuestionsRequest request);
    }
}