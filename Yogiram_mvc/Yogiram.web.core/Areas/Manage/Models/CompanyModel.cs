using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Manage.Models
{
    public class CompanyModel
    {
        public int? CompanyId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string CompanyName { get; set; }

        public string CompanyCode { get; set; }

        [Required(ErrorMessage = "Short Name is required.")]
        public string CompanyShortName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Please enter valid email format.")]
        public string OfficalMail { get; set; }

        [Required(ErrorMessage = "permanent address is required.")]
        public string  Address{ get; set; }

        public string TelephoneNo { get; set; }

        [Required(ErrorMessage = "Pincode is required.")]
        public int Pincode { get; set; }

        public string Country { get; set; }
    }
}