using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Yogiram.web.core.Startup))]
namespace Yogiram.web.core
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
