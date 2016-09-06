using System.ComponentModel.DataAnnotations;

namespace SaaSPro.Web.Management.ViewModels
{
    public class ForgotPasswordModel
    {
        [Required]
        public string Username { get; set; }
    }
}