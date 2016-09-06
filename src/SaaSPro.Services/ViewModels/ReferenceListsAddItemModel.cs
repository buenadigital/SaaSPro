using System;
using System.ComponentModel.DataAnnotations;

namespace SaaSPro.Services.ViewModels
{
    public class ReferenceListsAddItemModel
    {
        public Guid ReferenceListId { get; set; }
        
        [Required]
        public string Value { get; set; }
    }
}