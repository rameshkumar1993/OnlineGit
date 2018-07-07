using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Manage.Models
{
    public class RolesMenuModel
    {
        public int? RoleId { get; set; }

        [Required(ErrorMessage = "Role Name is required.")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Name should be only an alphabets.")]
        public string RoleName { get; set; }

        public Guid? ModuleId { get; set; }
    }
}