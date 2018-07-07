using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EIM.Models
{
    public class UserRegister
    {
        public int? EmployeeId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Name should be only an alphabets.")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Father's Name is required.")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Name should be only an alphabets.")]
        public string EmployeeFatherName { get; set; }

        [Required(ErrorMessage = "Mother's Name is required.")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Name should be only an alphabets.")]
        public string EmployeeMotherName { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Martial is required.")]
        public string Martial { get; set; }

        [Required(ErrorMessage = "permanent address is required.")]
        public string PerAddress { get; set; }

        [Required(ErrorMessage = "Permanent city is required.")]
        public string PerCity { get; set; }

        [Required(ErrorMessage = "Pincode is required.")]
        public int PerPincode { get; set; }

        [Required(ErrorMessage = "Mobile number is required.")]
        public string MobileNo { get; set; }

        public string TempAddress { get; set; }
        public string TempCity { get; set; }
        public int? TempPincode { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Please enter valid email format.")]
        public string Email { get; set; }

        public int? ManagerId { get; set; }
        public int? RoleId { get; set; }
        public string Password { get; set; }
        public int? DepartmentId { get; set; }

        [Required(ErrorMessage ="Date of join is required.")]
        public DateTime DOJ { get; set; }

        public bool EmpStatus { get; set; }
        public string EmployeeTermDate { get; set; }

        public string DesiginationId { get; set; }

        public HttpPostedFileBase Photo { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public string BloodGroup { get; set; }

        [Required(ErrorMessage = "Current password is required.")]
        [System.Web.Mvc.Remote("IsCurrentPassword", "Home", HttpMethod = "POST", ErrorMessage = "Your current password didn't match, please try again.")]
        public string CurrentPassword { get; set; }
        
        [Required(ErrorMessage = "New password is required.")]
        [System.Web.Mvc.Remote("IsValidPassword", "Home", HttpMethod = "POST", ErrorMessage = "Please choose a stronger password and try a mix of letter, numbers and symbols.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Re-Type New password is required.")]
        [Compare("NewPassword", ErrorMessage = "New password didn't match.")]
        public string ReTypeNewPassword { get; set; }

        public string sf()
        {
            return "sdf";
        }

    }

    public class RolesMenu
    {
        public Guid  MenuId { get; set; }
        public int? RoleId { get; set; }
        public string value { get; set; }
        public string label { get; set; }
        public string Checked { get; set; }
    }

}