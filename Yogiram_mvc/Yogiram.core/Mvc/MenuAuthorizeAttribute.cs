using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;
using Yogiram.core.Models;
using Yogiram.core.Context;
using Yogiram.core.Service;

namespace Yogiram.core.Mvc
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class MenuAuthorizeAttribute : LoginRequiredAttribute
    {
        public Guid[] MenuId { get; set; }

        public MenuItem Menu { get; set; }

        public MenuAuthorizeAttribute(params String[] MenuId)
        {
            if (MenuId.Length == 0)
                throw new ArgumentException("MenuId cannot be empty");

            this.MenuId = (from a in MenuId select Guid.Parse(a)).ToArray();
        }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            var Authorized = base.AuthorizeCore(httpContext);

            AccessDenied = false;

            if (Authorized)
            {
                HttpContext.Current.Session[session.MenuId] = MenuId;

                ContextContainer Context = new ContextContainer();

                MenuService menuService = new MenuService(Context);

                Authorized = menuService.IsUserInMenu(MenuId);

                //foreach (var menuId in MenuId)
                //{
                //    Authorized = menuService.IsMenuInRole(Context.CompanyId ?? 0, menuId);

                //    if (Authorized)
                //        break;
                //}


                AccessDenied = !Authorized;
            }

            return Authorized;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //filterContext.Controller.ViewBag.MenuId = MenuId.FirstOrDefault();

            var context = filterContext.HttpContext;

            if (string.IsNullOrEmpty(filterContext.HttpContext.User.Identity.Name))
                filterContext.Result = new RedirectResult(string.Format("~/Account/Login?ReturnUrl={0}", HttpUtility.UrlEncode(context.Request.RawUrl)));
            //return;
            //if (string.IsNullOrEmpty(filterContext.HttpContext.User.Identity.Name))
            //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { Controller = "Account", Action = "Login" }));
            //else
            //{
            //    // CustomPrincipal mp=new CustomPrincipal()
            //    // check valid role user

            //}
            base.OnAuthorization(filterContext);
        }

        public static ActionResult accessdeniedresult()
        {
            RouteValueDictionary route = new RouteValueDictionary();
            route.Add("controller", "Home");
            route.Add("action", "Index");
            route.Add("area", "");
            return new RedirectToRouteResult(route);
        }
    }
}
