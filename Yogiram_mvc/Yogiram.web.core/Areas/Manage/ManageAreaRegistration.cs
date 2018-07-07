using System.Web.Mvc;

namespace Yogiram.web.core.Areas.Manage
{
    public class ManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Manage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Manage_default",
                "Manage/{controller}/{action}/{id}",

                new { controller = "Manage", action = "Index", id = UrlParameter.Optional },
                new string[] { "Manage.Controllers" }
            );
        }
    }
}