using System;
using SaaSPro.Common;
using SaaSPro.Domain;

namespace SaaSPro.Services.ViewModels
{
    public class UsersListModel
    {
        public IPagedList<UserSummary> Users { get; set; }

        public class UserSummary
        {
            public Guid Id { get; set; }
            public string FullName { get; set; }
            public DateTime RegisteredDate { get; set; }
            public DateTime? LastLoginDate { get; set; }
            public string Email { get; set; }
            public string[] Roles { get; set; }
            public UserType UserType { get; set; }
        }
    }
}