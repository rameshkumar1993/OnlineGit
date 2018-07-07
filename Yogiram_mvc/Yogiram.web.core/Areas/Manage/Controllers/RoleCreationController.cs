using Manage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yogiram.core.DataSource;
using Yogiram.core.Models;
using Yogiram.core.Mvc;

namespace Manage.Controllers
{
    public class RoleCreationController : ManageController
    {
        [MenuAuthorize("EDCB74EA-10B1-4EB4-9E7A-C0FBE9668D08")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [MenuAuthorize("EDCB74EA-10B1-4EB4-9E7A-C0FBE9668D08")]
        public ActionResult Index(RolesMenuModel model)
        {
            try
            {
                AT_Roles objRoles = null;

                if (model.RoleId == null)
                {
                    objRoles = new AT_Roles();
                    objRoles.IsVisible = true;
                    objRoles.CreatedBy = User.Identity.Name;
                    objRoles.CreatedDate = DateTime.Now;

                    CoreContext.AT_Roles.Add(objRoles);
                }
                else
                {
                    objRoles = (from a in CoreContext.AT_Roles where a.RoleId == model.RoleId select a).FirstOrDefault();
                    objRoles.ModifiedBy = User.Identity.Name;
                    objRoles.ModifiedDate = DateTime.Now;
                }

                objRoles.Name = model.RoleName;

                CoreContext.SaveChanges();

                return Success(M("savedsucessfully"));
            }

            catch (Exception e)
            {
                return Error("InternalError");
            }
        }

        [HttpPost]
        [MenuAuthorize("EDCB74EA-10B1-4EB4-9E7A-C0FBE9668D08")]
        public ActionResult LoadRoleName(DtParams dt)
        {
            bool isEmpty = string.IsNullOrWhiteSpace(dt.search.value);

            var query = (from a in CoreContext.AT_Roles where (isEmpty || a.Name.Contains(dt.search.value)) select a);

            return DataTable(dt, query);

        }

        [HttpPost]
        [MenuAuthorize("EDCB74EA-10B1-4EB4-9E7A-C0FBE9668D08")]
        public ActionResult DeleteRole(int RoleId)
        {
            try
            {
                AT_Roles objRoles = (from a in CoreContext.AT_Roles where a.RoleId == RoleId select a).FirstOrDefault();
                CoreContext.AT_Roles.Remove(objRoles);
                CoreContext.SaveChanges();

                return Success(M("savedsucessfully"));
            }

            catch (Exception e)
            {
                return Error("InternalError");
            }

        }

        [HttpPost]
        [MenuAuthorize("8CE0ECD7-AE79-4288-8C38-B59FC75EEEFD")]
        public ActionResult MenuAssign(Guid[] MenuIds, int? RoleId)
        {
            AT_RolesForMenu objRolesMenu = null;

            try
            {
                var ExistRoleMenu = (from a in CoreContext.AT_RolesForMenu where a.RoleId == RoleId select a).ToList();

                foreach (var obj in ExistRoleMenu)
                {
                    CoreContext.AT_RolesForMenu.Remove(obj);
                }

                foreach (var MenuId in MenuIds)
                {
                    objRolesMenu = new AT_RolesForMenu();

                    objRolesMenu.MenuId = MenuId;
                    objRolesMenu.RoleId = RoleId ?? 0;
                    objRolesMenu.CreatedDate = DateTime.Now;
                    objRolesMenu.CreatedBy = User.Identity.Name;

                    CoreContext.AT_RolesForMenu.Add(objRolesMenu);
                }

                CoreContext.SaveChanges();

                return Success(M("savedsucessfully"));
            }
            catch (Exception e)
            {
                //log.Error("Internal Server error", e);
                return Error("An error occured while processing the request");
            }
        }

        [MenuAuthorize("8CE0ECD7-AE79-4288-8C38-B59FC75EEEFD")]
        public ActionResult MenuAssign()
        {
            return View();
        }

        [MenuAuthorize("8CE0ECD7-AE79-4288-8C38-B59FC75EEEFD")]
        public JsonResult RoleSelect2(string srch, int page)
        {
            var isEmpty = String.IsNullOrWhiteSpace(srch);

            var query = (from a in CoreContext.AT_Roles
                         where (isEmpty || a.Name.StartsWith(srch))
                         orderby a.Name
                         select new { id = a.RoleId, text = a.Name }).Skip((page - 1) * 10).Take(10);

            return Json(query, JsonRequestBehavior.AllowGet);
        }

        [MenuAuthorize("8CE0ECD7-AE79-4288-8C38-B59FC75EEEFD")]
        public JsonResult ModuleSelect2(string srch, int page)
        {
            var isEmpty = String.IsNullOrWhiteSpace(srch);

            var query = (from a in CoreContext.AT_Modules
                         where (isEmpty || a.ModuleName.StartsWith(srch))
                         orderby a.ModuleName
                         select new { id = a.ModuleId, text = a.ModuleName }).Skip((page - 1) * 10).Take(10);

            return Json(query, JsonRequestBehavior.AllowGet);
        }

        [MenuAuthorize("8CE0ECD7-AE79-4288-8C38-B59FC75EEEFD")]
        public JsonResult MenuRoleSelect2(int? RoleId, Guid? ModuleId)
        {
            var Menus = (from a in CoreContext.AT_Menu
                         where a.Url == null && a.ModuleId==ModuleId
                         select new
                         {
                             item = new { id = a.MenuId, label = a.MenuName, value = a.MenuId, Checked = false },
                             children = from b in CoreContext.AT_Menu where b.ParentMenuId == a.MenuId select new { item = new { id = b.MenuId, label = b.MenuName, value = b.MenuId, Checked = (from k in CoreContext.AT_RolesForMenu where k.MenuId == b.MenuId && k.RoleId == RoleId select k).Any() } }
                         }).ToList();

            return new JsonResult { Data = Menus };
        }

    }
}