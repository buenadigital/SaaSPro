using System;
using System.ComponentModel.DataAnnotations;
using SaaSPro.Domain;

namespace SaaSPro.Services.ViewModels
{
    public class RolesUpdateModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The role name is required.")]
        public string Name { get; set; }

        [Display(Name = "User Type", Description = "The type of users that can be assigned to this role.")]
        public UserType? UserType { get; set; }
    }
}