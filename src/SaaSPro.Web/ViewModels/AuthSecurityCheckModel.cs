using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SaaSPro.Web.ViewModels
{
    public class AuthSecurityCheckModel
    {
        [HiddenInput]
        public Guid Id { get; set; }
        public string Question { get; set; }       
        [Required]
        public string Answer { get; set; }
        public string ReturnUrl { get; set; }
    }
}