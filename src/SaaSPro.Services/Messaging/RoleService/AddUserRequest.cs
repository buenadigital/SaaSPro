using SaaSPro.Services.ViewModels;
using SaaSPro.Domain;

namespace SaaSPro.Services.Messaging.RoleService
{
    public class AddRoleRequest
    {
        public RolesUpdateModel RolesUpdateModel { get; set; }
        public Customer Customer { get; set; }
    }
}
