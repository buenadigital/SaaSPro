using System;
using System.Linq;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaasPro.TestsConfiguration;
using SaaSPro.Services.Interfaces;
using SaaSPro.Web.API.Controllers;
using SaaSPro.Web.API.Exceptions;
using SaaSPro.Web.API.Model.Auth;

namespace SaaSPro.Web.App.Tests
{
	[TestClass]
	public class ApiAuthControllerTests : TestsApiControlerBase<ApiAuthController>
	{
		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			ClassInitialize();
			Controller = new ApiAuthController(Container.GetInstance<IUserService>(), Container.GetInstance<IApiSessionTokenService>());
		    Controller.User = HttpContext.Current.User;
		}

		[TestInitialize]
		public override void TestInitialize()
		{
			base.TestInitialize();
		}

		[TestMethod]
		public void T01_Login()
		{
            // empty model checking
			var loginModel = new LoginModel();
            ValidateApiModel(loginModel);
		    try
		    {
                Controller.Login(loginModel);
                Assert.Fail("Empty login model passed test");
		    }
		    catch (Exception ex)
		    {
                Assert.IsInstanceOfType(ex, typeof(ModelException), "For empty login model incorrect type exception is thrown");
		    }
            Controller.ModelState.Clear();

            // incorrect email checking
            loginModel.Email = "wrong@email.com";
			loginModel.Password = "Qwerty123";
            ValidateApiModel(loginModel);
            try
            {
                Controller.Login(loginModel);
                Assert.Fail("Login with incorrect email passed test");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ApiException), "For wrong email incorrect type exception is thrown");
                Assert.AreEqual(((ApiException) ex).Error, ApiException.Errors.Auth.UserNotFound, "For wrong email incorrect error is returned");
            }
            Controller.ModelState.Clear();

            // incorrect password checking
            loginModel.Email = User.Email;
            loginModel.Password = "Wrong password";
            ValidateApiModel(loginModel);
            try
            {
                Controller.Login(loginModel);
                Assert.Fail("Login with incorrect password passed test");
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(ApiException), "For wrong password incorrect type exception is thrown");
                Assert.AreEqual(((ApiException)e).Error, ApiException.Errors.Auth.IncorrectPassword, "For wrong email incorrect error is returned");
            }
            Controller.ModelState.Clear();

            // correct input parameters checking
            loginModel.Email = User.Email;
            loginModel.Password = "Qwerty123";
            ValidateApiModel(loginModel);
            try
            {
                var result = Controller.Login(loginModel);
                Assert.IsTrue(!string.IsNullOrEmpty(result.Token), "Login with correct parameters returned empty token");
                Assert.IsTrue(!string.IsNullOrEmpty(result.SecurityQuestion), "Login with correct parameters returned empty security question");
            }
            catch
            {
                Assert.Fail("Login with correct parameters thrown exception");
            }
        }

	    [TestMethod]
	    public void T02_ValidateSecurityAnswer()
	    {
            // empty model checking
            var answerModel = new ValidateSecurityAnswerModel();
            ValidateApiModel(answerModel);
            try
            {
                Controller.ValidateSecurityAnswer(answerModel);
                Assert.Fail("Empty validate answer model passed test");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ModelException), "For empty validate answer model incorrect type exception is thrown");
            }
            Controller.ModelState.Clear();
            
            // incorrect answer checking
            ApiSessionToken.UpdateSecurityQuestion(User.SecurityQuestions.First());
            answerModel.Answer = "wrong";
            ValidateApiModel(answerModel);
            try
            {
                Controller.ValidateSecurityAnswer(answerModel);
                Assert.Fail("Validation with incorrect answer passed test");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ApiException), "For wrong answer incorrect type exception is thrown");
                Assert.AreEqual(((ApiException)ex).Error, ApiException.Errors.Auth.IncorrectSecurityAnswer, "For wrong answer incorrect error is returned");
            }
            Controller.ModelState.Clear();

            // correct answer checking
            ApiSessionToken.UpdateSecurityQuestion(User.SecurityQuestions.First());
            answerModel.Answer = "b";
            ValidateApiModel(answerModel);
            try
            {
                Controller.ValidateSecurityAnswer(answerModel);
            }
            catch (Exception ex)
            {
                Assert.Fail("Validation with correct answer not passed test");
            }
            Controller.ModelState.Clear();
        }

        [ClassCleanup]
		public static void ClassCleanup()
		{
			Cleanup();
		}
	}
}
