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
    public class CompanyRegistrationController : ManageController
    {
        [MenuAuthorize("7EF5A940-0C11-4304-8964-65ECD865FEA5")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [MenuAuthorize("7EF5A940-0C11-4304-8964-65ECD865FEA5")]
        public ActionResult Index(CompanyModel model)
        {
            try
            {
                CompanyMaster objCompanyMaster = null;

                if (model.CompanyId == null)
                {
                    objCompanyMaster = new CompanyMaster();
                    objCompanyMaster.CreatedBy = User.Identity.Name;
                    objCompanyMaster.CreatedDate = DateTime.Now;

                    CoreContext.CompanyMaster.Add(objCompanyMaster);
                }
                else
                {
                    objCompanyMaster = (from a in CoreContext.CompanyMaster where a.CompanyId == model.CompanyId select a).FirstOrDefault();
                    objCompanyMaster.ModifiedBy = User.Identity.Name;
                    objCompanyMaster.ModifiedDate = DateTime.Now;
                }

                objCompanyMaster.CompanyName = model.CompanyName;
                objCompanyMaster.CompanyShortName = model.CompanyShortName;
                objCompanyMaster.CompanyCode = model.CompanyCode;
                objCompanyMaster.Country = model.Country;
                objCompanyMaster.Address = model.Address;
                objCompanyMaster.TelphoneNo = model.TelephoneNo;
                objCompanyMaster.Pincode = model.Pincode;
                objCompanyMaster.OfficialMail = model.OfficalMail;
                
                CoreContext.SaveChanges();

                return Success(@M("savedsucessfully"));
            }

            catch (Exception e)
            {
                //log.Error("Internal Server error", e);
                return Error("InternalError");
            }
        }

        [HttpPost]
        [MenuAuthorize("7EF5A940-0C11-4304-8964-65ECD865FEA5")]
        public ActionResult LoadCompany(DtParams dt)
        {
            bool isEmpty = string.IsNullOrWhiteSpace(dt.search.value);

            var query = (from a in CoreContext.CompanyMaster
                         where (isEmpty || a.CompanyName.Contains(dt.search.value) || a.CompanyCode.Contains(dt.search.value)
                         || a.OfficialMail.Contains(dt.search.value))
                         select a);

            return DataTable(dt, query);
        }

        [HttpPost]
        [MenuAuthorize("7EF5A940-0C11-4304-8964-65ECD865FEA5")]
        public ActionResult LoadCompanyLogin()
        {
            var query = (from a in CoreContext.CompanyMaster select a).ToList();

            return Json(query, JsonRequestBehavior.AllowGet);
        }

    }
}