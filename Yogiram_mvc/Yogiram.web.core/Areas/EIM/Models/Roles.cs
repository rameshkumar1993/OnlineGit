using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EIM.Models
{
    public class Roles
    {
        [Required(ErrorMessage = "Role Name is required.")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Name should be only an alphabets.")]
        public string RoleName { get; set; }

        public int? RoleId { get; set; }

    }
}