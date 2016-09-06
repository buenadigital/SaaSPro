using System.ComponentModel.DataAnnotations;

namespace SaaSPro.Web.API.Model.Auth
{
    public class ValidateSecurityAnswerModel
    {
        [Required(ErrorMessage = "An answer is required")]
        public string Answer { get; set; }
    }
}