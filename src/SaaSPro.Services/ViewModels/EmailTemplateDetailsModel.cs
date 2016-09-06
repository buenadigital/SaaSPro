using System;
using System.ComponentModel.DataAnnotations;

namespace SaaSPro.Services.ViewModels
{
    public class EmailTemplateDetailsModel
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Template Name", Description = "The name of the email template.")]
        public string TemplateName { get; set; }

        [Required]
        [Display(Name="From Email", Description = "From Email")]
        public string FromEmail { get; set; }

        [Required]
        [Display(Name = "Subject", Description = "Subject of the email")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Body", Description = "Body of the email")]
        public string Body { get; set; }
    }
}