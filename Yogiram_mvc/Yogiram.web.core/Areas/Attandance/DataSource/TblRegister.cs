//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Attandance.DataSource
{
    using System;
    using System.Collections.Generic;
    
    public partial class TblRegister
    {
        public int RegId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public System.DateTime DOB { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifedBy { get; set; }
        public Nullable<int> RoleId { get; set; }
    }
}
