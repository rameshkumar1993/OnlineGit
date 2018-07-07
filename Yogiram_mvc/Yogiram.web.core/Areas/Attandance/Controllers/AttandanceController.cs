using Attandance.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Mvc;
using Yogiram.core.Mvc;


namespace Attandance.Controllers
{
    public class AttandanceController : BaseController
    {
        [MenuAuthorize("94D4A131-A441-46EC-B027-5363BF417233")]
        public ActionResult AttIndex()
        {
            return View();
        }

        protected virtual String M(String Key)
        {
            ResourceManager rm = new ResourceManager(typeof(AttandanceResource));
            string ModuleString = rm.GetString(Key);
            return ModuleString;
        }

    }
}