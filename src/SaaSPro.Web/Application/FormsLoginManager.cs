using System;
using System.Web;
using System.Web.Security;
using SaaSPro.Common.Helpers;

namespace SaaSPro.Web
{  
    public class FormsLoginManager : ILoginManager
    {
        private readonly HttpSessionStateBase session;

        public FormsLoginManager()
        {
            session = new HttpSessionStateWrapper(HttpContext.Current.Session);
        }
        
        public void LogIn(string username, bool persistent)
        {
            Ensure.Argument.NotNullOrEmpty(username, "username");
            FormsAuthentication.SetAuthCookie(username.ToLowerInvariant(), persistent);
            session[Constants.LoginSessionUserKey] = null;
        }

        public void LogOut()
        {
            FormsAuthentication.SignOut();
        }

        public Guid? GetLoginSessionUserId()
        {
            return session[Constants.LoginSessionUserKey] as Guid?;
        }

        public void SetLoginSessionUserId(Guid userId)
        {
            session[Constants.LoginSessionUserKey] = userId;
        }
    }
}