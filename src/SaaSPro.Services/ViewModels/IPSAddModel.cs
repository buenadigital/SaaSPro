using System.ComponentModel.DataAnnotations;

namespace SaaSPro.Services.ViewModels
{
    public class IPSAddModel
    {
        private const string IPRegex = @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
        
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Start IP")]
        [RegularExpression(IPRegex, ErrorMessage = "Invalid IP Address")]
        public string StartIPAddress { get; set; }

        [Required]
        [Display(Name = "End IP")]
        [RegularExpression(IPRegex, ErrorMessage = "Invalid IP Address")]
        public string EndIPAddress { get; set; }
    }
}