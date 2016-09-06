using System;
using System.ComponentModel.DataAnnotations;

namespace SaaSPro.Services.ViewModels
{
    public class ApiTokensUpdateModel
    {
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "The Api Token name is required.")]
        public string Name { get; set; }
    }
}
