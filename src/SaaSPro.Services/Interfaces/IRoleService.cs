using System;
using SaaSPro.Services.Messaging.RoleService;
using SaaSPro.Services.ViewModels;

namespace SaaSPro.Services.Interfaces
{
    public interface IRoleService
    {
        RolesListModel RolesList(Guid customerId);
        void AddRole(AddRoleRequest request);
        GetRoleResponse Get(GetRoleRequest request);
        DeleteRoleResponse DeleteRole(DeleteRoleRequest request);
        UpdateRoleResponse UpdateRole(UpdateRoleRequest request);
    }
}