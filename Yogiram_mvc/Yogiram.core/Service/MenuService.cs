using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yogiram.core.Context;
using Yogiram.core.DataSource;
using Yogiram.core.Models;

namespace Yogiram.core.Service
{
    public class MenuService
    {
        public ContextContainer Context { get; set; }

        private CoreEntities db;

        public MenuService(ContextContainer Context)
        {
            this.Context = Context;
            db = Context.CoreEntities;
        }

        //public MenuItem GetMenuItem(Guid MenuId)
        //{
        //    return GetMenuList(new Guid[] { MenuId }).FirstOrDefault();
            
        //}

        //public bool IsMenuInRole(int? RoleId)
        //{
        //    //var isSuperAdmin = Context.RoleService.IsUserSuperAdmin();

        //    var UserId = Context.UserId;

        //    var Menu= (from r in db.AT_RolesForMenu where r.RoleId==RoleId select r.MenuId).ToList();

        //    if(Menu!=null)
        //    {
        //       // GetMenuList(Menu)
        //    }
        //}

        public bool IsUserInMenu(Guid[] MenuIds)
        {
            var ModuleId = (from a in db.AT_Menu where MenuIds.Contains(a.MenuId) select a.ModuleId).FirstOrDefault();

            int? UserRoleId = (from a in db.AT_UserInRoles where a.ModuleId == ModuleId select a.RoleId).FirstOrDefault();

            bool IsMenu =(from a in db.AT_UserInRoles where a.RoleId==UserRoleId && a.UserId==Context.UserId select a).Any();

           // menuList = query;

            //var isSuperAdmin = Context.RoleService.IsUserSuperAdmin();

            //foreach (var module in query)
            //{
            //    //Type ResourcesType = null;

            //    //var ModuleId = module.Module;

            //    //if (ModuleId == null)
            //    //    ResourcesType = typeof(Global);
            //    //else
            //    //    ResourcesType = Context.GetModuleHandler(ModuleId.Value).ResourceManagerType;

            //    //var UserRoleIds = Context.RoleService.GetUserRoleIds(isSuperAdmin ? null : ModuleId);

            //   // menuList.AddRange(GetMenuList(CompanyId, isSuperAdmin, ResourcesType, (from a in db.OM_Menu where module.List.Contains(a.MenuId) select a), UserRoleIds, false));
            //}

            return IsMenu;
        }

        

    }
}
