using System.Web.Mvc;

namespace Yogiram.web.core.Areas.EIM
{
    public class EIMAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "EIM";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "EIM_default",
                "EIM/{controller}/{action}/{id}",
                new { controller = "EIM", action = "Index", id = UrlParameter.Optional },
                new string[] { "EIM.Controllers" }
            );
        }
    }
}