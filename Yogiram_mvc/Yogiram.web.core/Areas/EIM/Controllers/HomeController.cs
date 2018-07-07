using EIM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Yogiram.core.DataSource;
using Yogiram.core.Mvc;
using Yogiram.core.Service;

namespace EIM.Controllers
{
    public class HomeController : EIMController
    {
        [MenuAuthorize("D168C4AB-AB3D-4BF3-B20A-F125B44CB20C")]
        public ActionResult Index()
        {
            return View();
        }

        [MenuAuthorize("DA1066F0-9197-426E-8D89-B0BE918D36FE")]
        public ActionResult PasswordReset()
        {
            return View();
        }

        [MenuAuthorize("DA1066F0-9197-426E-8D89-B0BE918D36FE")]
        [HttpPost]
        public ActionResult PasswordReset(UserRegister model)
        {
            EmployeeMaster objEmpMaster = (from a in CoreContext.EmployeeMaster where a.EmployeeId == Context.UserId select a).FirstOrDefault();

            objEmpMaster.Password = LoginService.ComputeHash(model.NewPassword, null);
            objEmpMaster.Pass_Salt = (CreatePassword(16) + model.NewPassword);
            objEmpMaster.PassExpDate = DateTime.Now.AddDays(120);
            objEmpMaster.PassUpdateOn = DateTime.Now;

            CoreContext.SaveChanges();

            FormsAuthentication.SignOut();
            HttpContext.Session.Abandon();
            Session.Abandon();

            return Redirect("~/Account/Login");

        }

        [HttpPost]
        [MenuAuthorize("DA1066F0-9197-426E-8D89-B0BE918D36FE")]
        public ActionResult IsValidPassword(string NewPassword)
        {
            var IsPassword = NewPassword.Any(c => IsLetter(c)) && NewPassword.Any(c => IsDigit(c)) && NewPassword.Any(c => IsSymbol(c));
            return Json(IsPassword, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [MenuAuthorize("DA1066F0-9197-426E-8D89-B0BE918D36FE")]
        public ActionResult IsCurrentPassword(string CurrentPassword)
        {
            var EmpPassHash = (from a in CoreContext.EmployeeMaster where a.CompanyId == Context.CompanyId && a.EmployeeId == Context.UserId select a.Password).FirstOrDefault();
            var IsCurrPass = LoginService.VerifyHash(CurrentPassword, EmpPassHash);
            return Json(IsCurrPass, JsonRequestBehavior.AllowGet);
        }
    }
}