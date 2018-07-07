using Manage.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Mvc;
using Yogiram.core.DataSource;
using Yogiram.core.Mvc;
using Yogiram.core.Handler;

namespace Manage.Controllers
{
    public class ManageController : BaseController
    {
        public ModuleHandler Resource { get; private set; }

        public ManageController()
        {
            Resource = new ModuleHandler();
            Resource.ResourceManagerType = new ResourceManager(typeof(ManageResource));
        }

        public ActionResult ManageIndex()
        {
            return View();
        }

        public String M(String Key)
        {
            ResourceManager rm = new ResourceManager(typeof(ManageResource));
            string ModuleString = rm.GetString(Key);
            return ModuleString;
        }
    }
}