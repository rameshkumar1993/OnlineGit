using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Manage.Models
{
    public class HomeRegisterModels
    {
        public int? RegId { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required(ErrorMessage = "Date of birth field is required.")]
        public string DOB { get; set; }

        [Required(ErrorMessage = "Mobile number field is required.")]
        public string MobileNo { get; set; }

        [Required]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password field is required.")]
        public string ConfirmPassword { get; set; }

        public string Address { get; set; }
    }
}