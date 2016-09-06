using System;

namespace SaaSPro.Web
{
    public interface ILoginManager
    {
        void LogIn(string username, bool persistent);
        void LogOut();
        Guid? GetLoginSessionUserId();
        void SetLoginSessionUserId(Guid userId);
    }
}