using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Yogiram.core.Context;

namespace Yogiram.core.Mvc
{
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        public Guid? ModuleId { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var Authorized = false;

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                Authorized = false;
            }
            else if (!String.IsNullOrWhiteSpace(Roles))
            {
                ContextContainer Context = new ContextContainer();

                var UserRoles = (from a in Context.RoleService.GetUserInRoles(null) select a.ToLower()).ToList();

                var RequestRoles = (from a in Roles.Split(',') where a.Trim().Length > 0 select a.Trim().ToLower()).ToList();

                Authorized = (from a in RequestRoles where UserRoles.Contains(a) select a).Count() == RequestRoles.Count;
            }
            else
            {
                Authorized = true;
            }

            if (!Authorized)
                filterContext.Result = new HttpUnauthorizedResult(); 
        }
    }
}
