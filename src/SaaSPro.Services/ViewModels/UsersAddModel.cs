using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SaaSPro.Services.ViewModels
{
    public class UsersAddModel
    {       
        [Required]  
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address", Description = "The email address the user will use to log in.")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Security Question 1")]
        public string SecurityQuestion1 { get; set; }

        [Required]
        [Display(Name = "Security Answer 1")]
        public string SecurityAnswer1 { get; set; }

        [Required]
        [Display(Name = "Security Question 2")]
        public string SecurityQuestion2 { get; set; }

        [Required]
        [Display(Name = "Security Answer 2")]
        public string SecurityAnswer2 { get; set; }

        [Required]
        [Display(Name = "Security Question 3")]
        public string SecurityQuestion3 { get; set; }

        [Required]
        [Display(Name = "Security Answer 3")]
        public string SecurityAnswer3 { get; set; }

        public IEnumerable<SelectListItem> SecurityQuestions { get; set; }

        [Display(Name = "Select Roles")]
        public IEnumerable<Guid> SelectedRoles { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}