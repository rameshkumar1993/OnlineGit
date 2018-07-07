using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yogiram.core.Mvc;

namespace Yogiram.web.core.Controllers
{
    [RoleAuthorize(Roles = "SuperAdmin")]
    public class SuperAdminController : BaseController
    {
        // GET: SuperAdmin
        public ActionResult Index()
        {
            return View();
        }
    }
}