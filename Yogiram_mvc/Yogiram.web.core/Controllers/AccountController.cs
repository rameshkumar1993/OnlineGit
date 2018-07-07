using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Yogiram.web.core.Models;
using Yogiram.core.Models;
using System.Web.Security;
using Yogiram.core.DataSource;
using Yogiram.core.Context;
using Yogiram.core.Mvc;
using Yogiram.core.Service;


namespace Yogiram.web.core.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult Login(string returnUrl)
        {
            //ViewBag.ReturnUrl = returnUrl; // comment check

            AccountLogin model = new AccountLogin();

            model.ReturnUrl = returnUrl;

            model.SLCompanyId = new SelectList(CoreContext.CompanyMaster.ToList(), "CompanyId", "CompanyName");

            return View(model);
        }

        [HttpPost]
        public ActionResult Login(AccountLogin model)
        {
            AccountLogin models = new AccountLogin();

            if (ModelState.IsValid)
            {
                var Authenticate = Context.LoginService.Authenticate(model.CompanyId, model.Password, model.UserName);

                if (Authenticate != null)
                {
                    Session[session.EmployeeCode] = Authenticate.UserName;
                    Session[session.UserId] = Authenticate.UserId;
                    Session[session.UserName] = Authenticate.UserName;
                    Session[session.CompanyId] = model.CompanyId;

                    FormsAuthentication.SetAuthCookie(model.UserName, false);

                    if (Url.IsLocalUrl(model.ReturnUrl) && model.ReturnUrl.Length > 1 && model.ReturnUrl.StartsWith("/") && !model.ReturnUrl.StartsWith("//") && !model.ReturnUrl.StartsWith("*/\\*"))
                        return Redirect(model.ReturnUrl);
                    else
                    {
                        if (Authenticate.PasswordExpired)
                            return Redirect("/EIM/Home/PasswordReset");
                        else
                            return RedirectToAction("Index", "Home");
                    }
                }
                else
                    ViewBag.Error = "Invalid username or password.";

                models.SLCompanyId = new SelectList(CoreContext.CompanyMaster.ToList(), "CompanyId", "CompanyName");

            }

            return View(models);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            HttpContext.Session.Abandon();
            Session.Abandon();
            return RedirectToAction("Login");
        }

    }
}