using EIM.Models;
using EIM.Resource;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Security.AccessControl;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Yogiram.core.DataSource;
using Yogiram.core.Models;
using Yogiram.core.Mvc;
using Yogiram.core.Service;


namespace EIM.Controllers
{
    public class UserRegistrationController : EIMController
    {
        [MenuAuthorize("DA1066F0-9197-426E-8D89-B0BE918D36FE")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [MenuAuthorize("DA1066F0-9197-426E-8D89-B0BE918D36FE")]
        public ActionResult Index(UserRegister model)
        {
            try
            {
                EmployeeMaster objEmployeeMaster = null;
                string sEmployeeCode = EmployeeCode();
                string PhysicalPath = "";
                LoginService ls = new LoginService(Context);

                if (model.EmployeeId == null)
                {
                    objEmployeeMaster = new EmployeeMaster();
                    objEmployeeMaster.EmployeeCode = sEmployeeCode;
                    string Pass = CreatePassword(8);
                    objEmployeeMaster.Password = LoginService.ComputeHash(Pass, null);
                    objEmployeeMaster.Pass_Salt = (CreatePassword(16) + Pass);

                    objEmployeeMaster.CreatedBy = User.Identity.Name;
                    objEmployeeMaster.PassExpDate = DateTime.Now;
                    objEmployeeMaster.CreatedDate = DateTime.Now;

                    if (model.Photo != null)
                    {
                        PhysicalPath = Server.MapPath("~/Images/Employee Photo/" + sEmployeeCode + ".jpg");
                        model.Photo.SaveAs(PhysicalPath);
                        objEmployeeMaster.Photo = "../Images/Employee Photo/" + sEmployeeCode + ".jpg";
                    }

                    CoreContext.EmployeeMaster.Add(objEmployeeMaster);
                }
                else
                {
                    objEmployeeMaster = (from a in CoreContext.EmployeeMaster where a.EmployeeId == model.EmployeeId select a).FirstOrDefault();

                    PhysicalPath = Server.MapPath("~/Images/Employee Photo/" + objEmployeeMaster.EmployeeCode + ".jpg");

                    if (System.IO.File.Exists(PhysicalPath))
                    {
                        System.IO.File.Delete(PhysicalPath);
                        objEmployeeMaster.Photo = null;
                    }

                    if (model.Photo != null)
                    {
                        model.Photo.SaveAs(PhysicalPath);
                        objEmployeeMaster.Photo = "../Images/Employee Photo/" + objEmployeeMaster.EmployeeCode + ".jpg";
                    }

                    objEmployeeMaster.ModifiedBy = User.Identity.Name;
                    objEmployeeMaster.ModifiedDate = DateTime.Now;
                }

                objEmployeeMaster.EmployeeName = model.EmployeeName;
                objEmployeeMaster.FathersName = model.EmployeeFatherName;
                objEmployeeMaster.MothersName = model.EmployeeMotherName;
                objEmployeeMaster.DateofBirth = Convert.ToDateTime(model.DOB);
                objEmployeeMaster.Gender = model.Gender;
                objEmployeeMaster.MartialStatus = model.Martial;
                objEmployeeMaster.Email = model.Email;
                objEmployeeMaster.MobileNo = Convert.ToDecimal(model.MobileNo);
                objEmployeeMaster.DateofJoin = model.DOJ;
                objEmployeeMaster.PerAddress = model.PerAddress;
                objEmployeeMaster.PerCity = model.PerCity;
                objEmployeeMaster.PerPincode = model.PerPincode;
                objEmployeeMaster.TempAddress = model.TempAddress;
                objEmployeeMaster.TempCity = model.TempCity;
                objEmployeeMaster.TempPincode = model.TempPincode;
                objEmployeeMaster.EmpDescription = model.Description;
                objEmployeeMaster.BloodGroup = model.BloodGroup;
                objEmployeeMaster.CompanyId = Context.CompanyId ?? 0;

                objEmployeeMaster.EmployeeStatus = model.EmpStatus == true ? "L" : "R";

                CoreContext.SaveChanges();

                return Success(M("savedsucessfully"));
            }

            catch (Exception e)
            {
                //log.Error("Internal Server error", e);
                return Error("InternalError");
            }

        }

        public JsonResult DepartmentSelect(string srch, int page)
        {

            var isEmpty = String.IsNullOrWhiteSpace(srch);

            var query = (from a in CoreContext.AT_Menu
                         where (isEmpty || a.MenuName.StartsWith(srch))
                         orderby a.MenuName
                         select new { id = a.MenuId, text = a.MenuName }).Skip((page - 1) * 10).Take(10);

            return Json(query, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [MenuAuthorize("DA1066F0-9197-426E-8D89-B0BE918D36FE")]
        public ActionResult LoadData(DtParams dt)
        {
            bool isEmpty = string.IsNullOrWhiteSpace(dt.search.value);

            var query = (from a in CoreContext.EmployeeMaster
                         where (isEmpty || a.EmployeeName.Contains(dt.search.value) || a.EmployeeCode.Contains(dt.search.value)
                         || a.Email.Contains(dt.search.value))
                         select a);

            return DataTable(dt, query);
        }

    }
}