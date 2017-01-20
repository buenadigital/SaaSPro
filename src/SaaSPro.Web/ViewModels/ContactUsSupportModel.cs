using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SaaSPro.Web.ViewModels
{
    public class ContactUsSupportModel
    {
        [Display(Name = "Name")]
        [Required()]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Message")]
        [Required()]
        [StringLength(200)]
        public string Message { get; set; }

        [Display(Name = "Email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [Display(Name = "Phone")]
        [Required()]
        [StringLength(25)]
        public string Phone { get; set; }

        public string To { get; set; }
        public string FromEmail { get; set; }
        public string Subject { get; set; }


    }
}