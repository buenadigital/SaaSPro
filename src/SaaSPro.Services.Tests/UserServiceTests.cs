using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaasPro.TestsConfiguration;
using SaasPro.TestsConfiguration.Helper;
using SaaSPro.Data;
using SaaSPro.Data.Repositories;
using SaaSPro.Domain;
using SaaSPro.Services.Interfaces;
using SaaSPro.Services.Messaging.UserService;
using SaaSPro.Services.ViewModels;

namespace SaaSPro.Services.Tests
{
	[TestClass]
	public class UserServiceTests : TestsConfiguration
	{
		private IUserService _userService => Container.GetInstance<IUserService>();

		private static User _user;
		private static Customer _customer;

		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			ClassInitialize();
			_user = TestsDataInitialize.CreateUser(Container.GetInstance<EFDbContext>());
			var customerRepository = Container.GetInstance<ICustomerRepository>();
			_customer = customerRepository.Get(_user.CustomerId);
		}

		[TestMethod]
		public void T01_GetUser()
		{
			var user = _userService.GetUser(new GetUserRequest
			{
				CustomerId = _customer.Id,
				Email = _user.Email,
				GetBy = "EMAIL"
			});
			Assert.IsTrue(user.Id == _user.Id, "Can't get correct user by GetUserRequest");
		}

		[TestMethod]
		public void T02_GetLoginSessionUser()
		{
			var user = _userService.GetLoginSessionUser(_user.Id);
			Assert.IsTrue(user.Id == _user.Id, "Can't get correct user by id");
		}

		[TestMethod]
		public void T03_UserModifications()
		{
			var user = _userService.Add(new AddUserRequest
			{
				Customer = _customer,
				CustomerId  = _customer.Id,
				Model = new UsersAddModel
				{
					Password = "Qwerty456",
					ConfirmPassword = "Qwerty456",
					Email = "seconduser@email.com",
					FirstName = "Pole",
					LastName = "Anderson",
					SecurityQuestion1 = "a",
					SecurityQuestion2 = "b",
					SecurityQuestion3 = "c",
					SecurityAnswer1 = "a",
					SecurityAnswer2 = "b",
					SecurityAnswer3 = "c"
				}
			});

			var userRepository = Container.GetInstance<IUserRepository>();
			var addedUser = userRepository.Get(user.UserID);
			Assert.IsNotNull(addedUser, "Can't create user by AddUserRequest");

			
			var securityQuestionsResponse = _userService.GetSecurityQuestions(new GetSecurityQuestionsRequest { CustomerId = _customer.Id, Id = user.UserID });
			Assert.AreEqual(securityQuestionsResponse.UsersUpdateSecurityQuestionsModel.SecurityQuestions.Count(), 3, "Can't get correct securities questions");

			// TODO: fix bug with Update security questions
			/*
			var questions = securityQuestionsResponse.UsersUpdateSecurityQuestionsModel.SecurityQuestions;
            foreach (var question in questions)
			{
				question.Question = question.Id.ToString();
			}

			_userService.UpdateSecurityQuestions(new UpdateSecurityQuestionsRequest { CustomerId = _customer.Id, Id = user.UserID, UsersUpdateSecurityQuestionsModel = securityQuestionsResponse.UsersUpdateSecurityQuestionsModel } );
			securityQuestionsResponse = _userService.GetSecurityQuestions(new GetSecurityQuestionsRequest { CustomerId = _customer.Id, Id = user.UserID });
			questions = securityQuestionsResponse.UsersUpdateSecurityQuestionsModel.SecurityQuestions;
			foreach (var question in questions)
			{
				Assert.AreEqual(question.Question, question.Id.ToString(), "Update security questions method don't work correct");
			}*/

			_userService.Delete(new DeleteUserRequest { Id = user.UserID });
			var deletedUser = userRepository.Get(user.UserID);
			Assert.IsNull(deletedUser, "Can't delete user by DeleteUserRequest");
		}

		[TestMethod]
		public void T04_GetLoginSessionUser()
		{
			_userService.SetPassword(_user.Id, "123Qwerty", true);
			var userRepository = Container.GetInstance<IUserRepository>();
			var user = userRepository.Get(_user.Id);
			Assert.IsTrue(user.HasExpiredPassword, "Can't correct change password");
		}

		/*
		UpdateProfileResponse UpdateProfile(UpdateProfileRequest model);
		GetUserProfileResponse GetUserProfile(GetUserProfileRequest request);
		UsersAddModel AddUserModel(Guid customerId, UserType userType);
		*/

		[TestMethod]
		public void T05_ProfileModification()
		{
			var userProfile = _userService.GetUserProfile(new GetUserProfileRequest { CustomerId = _customer.Id, Id = _user.Id });
			Assert.IsNotNull(userProfile, "Get user profile method don't work correct");

			userProfile.UsersUpdateModel.Email = "newuser@mail.com";

			_userService.UpdateProfile(new UpdateProfileRequest { CustomerId = _customer.Id, Id = _user.Id, UsersUpdateModel = userProfile.UsersUpdateModel });
			userProfile = _userService.GetUserProfile(new GetUserProfileRequest { CustomerId = _customer.Id, Id = _user.Id });
			Assert.AreEqual("newuser@mail.com", userProfile.UsersUpdateModel.Email, "Update user profile method don't work correct");
		}

		[ClassCleanup()]
		public static void ClassCleanup()
		{
			Cleanup();
		}
	}
}
