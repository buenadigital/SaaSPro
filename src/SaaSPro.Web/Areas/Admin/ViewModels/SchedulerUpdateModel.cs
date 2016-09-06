using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SaaSPro.Web.Areas.Admin.ViewModels
{
    public class SchedulerUpdateModel
    {
        public string Name { get; set; }
        public IDictionary<string, string> Properties { get; set; }
        
        [Display(Name = "Repeat Interval (minutes)")]
        [Range(1, 60, ErrorMessage = "The repeat interval must be between 5 and 60 minutes.")]
        public int RepeatInterval { get; set; }
    }
}