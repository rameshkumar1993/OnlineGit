using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yogiram.core.Context;
using Yogiram.core.DataSource;

namespace Yogiram.core.Service
{
    public class RoleService
    {
        public static readonly Guid ApplicationId = Guid.Parse("47425ADB-0507-4D5E-962D-05175AA15947");

        public ContextContainer Context { get; set; }

        private CoreEntities db;

        private Dictionary<Guid?, String[]> _UserInRoles = new Dictionary<Guid?, string[]>();

        public RoleService(ContextContainer Context)
        {
            this.Context = Context;
            db = Context.CoreEntities;
        }

        public bool IsUserInRole(String Role)
        {
            var UserId = Context.UserId;
            
            return true;
        }


        public String[] GetUserInRoles(Guid? ModuleId)
        {
            var UserId = Context.UserId;

            if (UserId == null)
                return new String[] { };

            var mId = ModuleId ?? ApplicationId;

            if (!_UserInRoles.ContainsKey(mId))
                _UserInRoles[mId] = (from a in db.AT_UserInRoles where a.UserId == UserId.Value select a.AT_Roles.Name).ToArray();

            return _UserInRoles[mId];
        }

        //public bool IsUserInRole(String Role, Guid? ModuleId, int? UserId)
        //{
        //    if (UserId == null)
        //        return false;

        //    int? hasRole = (from a in db.AT_UserInRoles where a.UserId == UserId select a.RoleId).FirstOrDefault();

        //    if (hasRole!=null)
        //    {
        //        var ismenurole = Context.MenuService.IsMenuInRole(hasRole);
        //    }
                

        //    var hasRole = (from a in db.OM_UserInRoles where a.UserId == UserId.Value && a.OM_Roles.Name == Role && a.ModuleId == ModuleId select a).Count() > 0;

        //    if (!HasRolesCache.ContainsKey(mId))
        //        HasRolesCache[mId] = new Dictionary<string, bool>(StringComparer.InvariantCultureIgnoreCase);

        //    HasRolesCache[mId][Role] = hasRole;

        //    return hasRole;
        //}
    }
}
