using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using Yogiram.core.Models;
using System.Web.Routing;


namespace Yogiram.core.Mvc
{
    public class LoginRequiredAttribute : AuthorizeAttribute
    {
        protected bool AccessDenied { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return base.AuthorizeCore(httpContext) && httpContext.Session[session.UserId] != null;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var acceptType = filterContext.RequestContext.HttpContext.Request.Headers["Accept"];

            bool requiredJson = (acceptType ?? String.Empty).Contains("application/json");

            var context = filterContext.HttpContext;

            if (AccessDenied)
            {
                filterContext.HttpContext.Response.AddHeader("AccessDenied", "true");

                if (requiredJson)
                {
                    filterContext.Result = new NewtonsoftJsonResult()
                    {
                        Data = new ErrorJsonModel() { error = "AccessDenied" },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                    filterContext.Result = AccessDeniedResult();

            }
            else
            {
                if (requiredJson)
                {
                    if (filterContext.HttpContext.Request.QueryString["draw"] != null && filterContext.HttpContext.Request.QueryString["columns[0].name"] != null)
                    {
                        filterContext.Result = new NewtonsoftJsonResult()
                        {
                            Data = new { draw = Convert.ToInt32(filterContext.HttpContext.Request.QueryString["draw"]), sessionExpired = true, data = new List<object>(), recordsTotal = 0, recordsFiltered = 0 },
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                    else
                    {
                        filterContext.Result = new NewtonsoftJsonResult()
                        {
                            Data = new ErrorSessionExpiredModel { error = "SessionExpired" },
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                }
                else
                    base.HandleUnauthorizedRequest(filterContext);
            }
           
            if (string.IsNullOrEmpty(filterContext.HttpContext.User.Identity.Name))
                filterContext.Result = new RedirectResult(string.Format("~/Account/Login?ReturnUrl={0}", HttpUtility.UrlEncode(context.Request.RawUrl)));
        }

        public static ActionResult AccessDeniedResult()
        {
            RouteValueDictionary route = new RouteValueDictionary();
            route.Add("controller", "Home");
            route.Add("action", "UnAuthorize");
            route.Add("area", "");
            return new RedirectToRouteResult(route);
        }
    }
}
