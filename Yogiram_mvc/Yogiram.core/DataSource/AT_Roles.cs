//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Yogiram.core.DataSource
{
    using System;
    using System.Collections.Generic;
    
    public partial class AT_Roles
    {
        public AT_Roles()
        {
            this.AT_RolesForMenu = new HashSet<AT_RolesForMenu>();
            this.AT_UserInRoles = new HashSet<AT_UserInRoles>();
        }
    
        public int RoleId { get; set; }
        public string Name { get; set; }
        public Nullable<bool> IsVisible { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual ICollection<AT_RolesForMenu> AT_RolesForMenu { get; set; }
        public virtual ICollection<AT_UserInRoles> AT_UserInRoles { get; set; }
    }
}
