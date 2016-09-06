using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SaaSPro.Services.ViewModels
{
    public class UsersUpdateModel
    {       
        public Guid Id { get; set; }
        
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email Address", Description = "The email address the user will use to log in.")]
        public string Email { get; set; }
        
        [Display(Name = "Security Questions")]
        public IEnumerable<SecurityQuestion> SecurityQuestions { get; set; }
        public IEnumerable<SelectListItem> SecurityQuestionOptions { get; set; }

        [Display(Name = "Select Roles")]
        public IEnumerable<Guid> SelectedRoles { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
              
        public void UpdateQuestionIndexes()
        {
            int i = 1;
            foreach (var question in SecurityQuestions)
            {
                question.Index = i;
                i++;
            }
        }
    }

    public class SecurityQuestion
    {
        public Guid Id { get; set; }
        public int Index { get; set; }

        [Required]
        public string Question { get; set; }

        public string Answer { get; set; }
    }
}