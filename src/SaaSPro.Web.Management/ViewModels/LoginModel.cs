using System.ComponentModel.DataAnnotations;

namespace SaaSPro.Web.Management.ViewModels
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}