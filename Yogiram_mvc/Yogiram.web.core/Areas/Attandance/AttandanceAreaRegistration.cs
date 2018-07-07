using System.Web.Mvc;

namespace Yogiram.web.core
{
    public class AttandanceAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Attandance";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Attandance_default",
                "Attandance/{controller}/{action}/{id}",

                new { controller="Attandance", action = "Index", id = UrlParameter.Optional },
                new string[] { "Attandance.Controllers" }
            );
        }
    }
}