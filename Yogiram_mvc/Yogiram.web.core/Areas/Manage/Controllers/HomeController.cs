using Manage.DataSource;
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
    public class HomeController : ManageController
    {
        [MenuAuthorize("F128AC08-9CAF-4A20-A244-5582C6DA32F2")]
        public ActionResult Index()
        {
            return View();
        }

        //[MenuAuthorize("D79DB074-8067-4084-A03A-E194F5A5827E")]
        [HttpPost]
        public ActionResult LoadData(DtParams dt)
        {
            bool isEmpty = string.IsNullOrWhiteSpace(dt.search.value);

            var query = (from a in CoreContext.TblRegister 
                         where (isEmpty || a.Name.Contains(dt.search.value) || a.Email.Contains(dt.search.value)
                         || a.Mobile.Contains(dt.search.value))
                         select a);

            return DataTable(dt, query);

        }

    }
}