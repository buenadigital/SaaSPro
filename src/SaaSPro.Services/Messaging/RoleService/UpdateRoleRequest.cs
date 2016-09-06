using System;
using SaaSPro.Services.ViewModels;


namespace SaaSPro.Services.Messaging.RoleService
{
    public class UpdateRoleRequest
    {
        public Guid Id { get; set; }
        public RolesUpdateModel RolesUpdateModel { get; set; }
    }
}
