using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yogiram.core.Mvc;

namespace Yogiram.web.core.Controllers
{
    [SessionState(System.Web.SessionState.SessionStateBehavior.ReadOnly)]
    public class HomeController : ModuleController
    {
        [LoginRequired]
        public ActionResult Index()
        {
            bool isSuperAdmin = Context.UserId < 0;
            //bool isSuperAdmin = Context.RoleService.IsUserInRole("SuperAdmin");

            if (isSuperAdmin)
                return RedirectToAction("Index", "SuperAdmin");

            SkipMenuLoad = true;

            return View();
        }
        public string UnAuthorize()
        {
            return "UnAuthorize";
        }
    }
}