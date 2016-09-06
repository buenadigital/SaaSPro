using SaaSPro.Services.ViewModels;

namespace SaaSPro.Services.Messaging.RoleService
{
   public class GetRoleResponse:BaseResponse
    {
       public RolesUpdateModel RolesUpdateModel { get; set; }
       public bool SystemRole { get; set; }
    }
}
