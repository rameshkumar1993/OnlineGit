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
    
    public partial class AT_UserInRoles
    {
        public System.Guid UserInRoleId { get; set; }
        public Nullable<System.Guid> ModuleId { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    
        public virtual AT_Roles AT_Roles { get; set; }
    }
}
