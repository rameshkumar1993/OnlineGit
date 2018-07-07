using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Yogiram.web.core.Models
{
    public class AccountLogin
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public int RegId { get; set; }

        public string Email { get; set; }

        public string ErrorMessage { get; set; }

        public string[] Roles { get; set; }

        public SelectList SLCompanyId { get; set; }

        public int? CompanyId { get; set; }

 
       public string ReturnUrl { get; set; }

    }

    
}