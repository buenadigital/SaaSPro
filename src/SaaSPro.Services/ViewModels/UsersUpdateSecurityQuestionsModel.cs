using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SaaSPro.Services.ViewModels
{
    public class UsersUpdateSecurityQuestionsModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Security Questions")]
        public IEnumerable<SecurityQuestion> SecurityQuestions { get; set; }
        public IEnumerable<SelectListItem> SecurityQuestionOptions { get; set; }
        
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
}
