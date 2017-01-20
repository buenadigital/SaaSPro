using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SaaSPro.Common.Helpers;
using SaaSPro.Services.Helpers;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.Messaging.UserService;
using SaaSPro.Services.ViewModels;
using SaaSPro.Common;
using SaaSPro.Data.Repositories;
using SaaSPro.Domain;
using SaaSPro.Web.Common;
using AutoMapper;


namespace SaaSPro.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleRepository _roleRepository;
        private readonly IReferenceListRepository _referenceListRepository;
        private const int PasswordExpiryDays = 90;
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IRoleRepository roleRepository,IReferenceListRepository referenceListRepository)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _roleRepository = roleRepository;
            _referenceListRepository = referenceListRepository;
        }

        public User GetUser(GetUserRequest request)
        {
            var user = request.GetBy == "EMAIL" 
                ? _userRepository.Query().SingleOrDefault(u => u.Email == request.Email.ToLower()) 
                : _userRepository.Query().SingleOrDefault(u => u.Email == request.Email.ToLower() && u.CustomerId == request.CustomerId);
            return user;
        }

        public User GetLoginSessionUser(Guid? userId)
        {
            if (userId.HasValue)
            {
                return _userRepository.Get(userId.Value);
            }
            return null;
        }


        public UsersListModel List(PagingCommand command)
        {
            var users = _userRepository.FetchPaged(
                q => q.Where(u => u.UserTypeString == command.UserType.ToString() && u.CustomerId == command.CustomerId).OrderBy(u => u.FirstName).ThenBy(u => u.LastName),
                command.PageIndex,
                command.PageSize
            );

            var model = new UsersListModel
            {
                Users = Mapper.Engine.MapPaged<User, UsersListModel.UserSummary>(users)
            };
            return model;
        }

        public DeleteUserResponse Delete(DeleteUserRequest request)
        {
            DeleteUserResponse response = new DeleteUserResponse();

            User user = _userRepository.Get(request.Id);

            if (user == null)
            {
                response.HasError = true;
                response.Message = "User not found";
                return response;
            }

            if (user.Id == request.CurrentUserID)
            {
                throw new InvalidOperationException("You cannot delete yourself!");
            }

            // must remove all role memberships before deleting
            user.RemoveFromAllRoles();
            _userRepository.Delete(user);

            _unitOfWork.Commit();

            return response;
        }

        public AddUserResponse Add(AddUserRequest request)
        {
           AddUserResponse response=new AddUserResponse();

            var user = new User(request.Customer, request.Model.Email, request.Model.FirstName, request.Model.LastName, request.Model.Password);
            user.AddSecurityQuestion(request.Model.SecurityQuestion1, request.Model.SecurityAnswer1);
            user.AddSecurityQuestion(request.Model.SecurityQuestion2, request.Model.SecurityAnswer2);
            user.AddSecurityQuestion(request.Model.SecurityQuestion3, request.Model.SecurityAnswer3);

            UpdateRoleMembership(user, request.Model.SelectedRoles,request.CustomerId);
            _userRepository.Add(user);
     
            _unitOfWork.Commit();

            response.UserID = user.Id;

            return response;
        }

        public void SetPassword(Guid userId,string newPassword, bool expireImmediately = false)
        {
            User user = _userRepository.Get(userId);

            Ensure.Argument.NotNullOrEmpty(newPassword, "newPassword");
            user.Password = CryptoHelper.HashPassword(newPassword);
            user.PasswordExpiryDate = expireImmediately ? DateTime.UtcNow : DateTime.UtcNow.AddDays(PasswordExpiryDays);
            user.UpdatedBy = user;
            user.UpdatedOn = DateTime.Now;
            _unitOfWork.Commit();
 
        }

        public UpdateProfileResponse UpdateProfile(UpdateProfileRequest request)
        {
            UpdateProfileResponse response = new UpdateProfileResponse();

            User user = _userRepository.Get(request.Id);

            if (user == null)
            {
                response.HasError = true;
                return response;
            }

            user.Email = request.UsersUpdateModel.Email;
            user.FirstName = request.UsersUpdateModel.FirstName;
            user.LastName = request.UsersUpdateModel.LastName;

            // Security Questions
            foreach (var question in request.UsersUpdateModel.SecurityQuestions.Where(q => q.Answer.IsNotNullOrEmpty()))
            {
                user.UpdateQuestion(question.Id, question.Question, question.Answer);
            }

            _roleRepository.Query().Where(r => r.CustomerId == request.CustomerId).ToList().Map(
                request.UsersUpdateModel.SelectedRoles,
                r => r.AddUser(user),
                r => r.RemoveUser(user)
                );

            _unitOfWork.Commit();

            return response;
        }

        public GetUserProfileResponse GetUserProfile(GetUserProfileRequest request)
        {
            GetUserProfileResponse response=new GetUserProfileResponse();

            User user = _userRepository.Get(request.Id);

            response.UsersUpdateModel= Mapper.Map<UsersUpdateModel>(user);
            response.UsersUpdateModel.SecurityQuestionOptions = new SelectList(GetSecurityQuestions(request.CustomerId));
            response.UsersUpdateModel.UpdateQuestionIndexes();
            response.UsersUpdateModel.Roles = GetRolesSelectList(request.UserType,request.CustomerId);

            return response;
        }

        public UsersAddModel AddUserModel(Guid customerId, UserType userType)
        {
            var securityQuestions = GetSecurityQuestions(customerId);

            var model = new UsersAddModel
                            {
                                Roles = GetRolesSelectList(userType, customerId),
                                SecurityQuestions = new SelectList(securityQuestions),
                                SecurityQuestion1 = securityQuestions[0],
                                SecurityQuestion2 = securityQuestions[1],
                                SecurityQuestion3 = securityQuestions[2]
                            };

            return model;
        }

        public GetSecurityQuestionsResponse GetSecurityQuestions(GetSecurityQuestionsRequest request)
        {
            GetSecurityQuestionsResponse response = new GetSecurityQuestionsResponse();

            User user = _userRepository.Get(request.Id);

            response.UsersUpdateSecurityQuestionsModel = Mapper.Map<UsersUpdateSecurityQuestionsModel>(user);
            response.UsersUpdateSecurityQuestionsModel.SecurityQuestionOptions = new SelectList(GetSecurityQuestions(request.CustomerId));
            response.UsersUpdateSecurityQuestionsModel.UpdateQuestionIndexes();

            return response;
        }

        public UpdateSecurityQuestionsResponse UpdateSecurityQuestions(UpdateSecurityQuestionsRequest request)
        {
            UpdateSecurityQuestionsResponse response = new UpdateSecurityQuestionsResponse();

            User user = _userRepository.Get(request.Id);

            if (user == null)
            {
                response.HasError = true;
                return response;
            }

            // checking dublicates
            var securityQuestions = request.UsersUpdateSecurityQuestionsModel.SecurityQuestions.ToArray();
            for (var i = 0; i < securityQuestions.Length - 1; i++)
            {
                for (var j = i + 1; j < securityQuestions.Length; j++)
                {
                    if (securityQuestions[i].Question == securityQuestions[j].Question)
                    {
                        throw new ArgumentException("The question '{0}' already exists.".FormatWith(securityQuestions[j].Question));
                    }
                }
            }
            
            foreach (var question in request.UsersUpdateSecurityQuestionsModel.SecurityQuestions.Where(q => q.Answer.IsNotNullOrEmpty()))
            {
                user.UpdateQuestion(question.Id, question.Question, question.Answer);
            }

            _unitOfWork.Commit();

            return response;
        }


        internal string[] GetSecurityQuestions(Guid customerId)
        {
            var referenceList = _referenceListRepository.GetBySystemName(ReferenceLists.SecurityQuestions);
            return referenceList.Items.Where(i => i.CustomerId == customerId).Select(x => x.Value).ToArray();
        }

        internal SelectList GetRolesSelectList(UserType userType, Guid customerId)
        {
            var roles = _roleRepository.Fetch(q =>
                q.Where(r => (r.UserTypeString == userType.ToString()) && r.CustomerId == customerId).OrderBy(r => r.Name));
            return new SelectList(roles, "Id", "Name");
        }
        internal void UpdateRoleMembership(User user, IEnumerable<Guid> selectedRoles, Guid customerId)
        {
            _roleRepository.Query().Where(r => r.CustomerId == customerId).ToList().Map(
               selectedRoles,
                r => r.AddUser(user),
                r => r.RemoveUser(user)
            );
        }
    }
}
