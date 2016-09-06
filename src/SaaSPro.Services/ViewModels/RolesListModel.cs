using System;
using System.Collections.Generic;
using SaaSPro.Common.Helpers;
using SaaSPro.Domain;

namespace SaaSPro.Services.ViewModels
{
    public class RolesListModel
    {
        public IEnumerable<RoleSummary> Roles { get; set; } 

        public class RoleSummary
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public UserType? UserType { get; set; }
            public bool SystemRole { get; set; }

            public string UserTypeName => UserType?.ToString().SeparatePascalCase();
        }
    }
}