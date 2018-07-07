using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yogiram.core.Mvc;

namespace Attandance.Controllers
{
    public class HomeController : AttandanceController
    {
        [MenuAuthorize("D79DB074-8067-4084-A03A-E194F5A5827E")]
        public ActionResult Index()
        {
            return View();
        }

        //public JsonResult CalandarBind()
        //{
            
        //}

    }
}