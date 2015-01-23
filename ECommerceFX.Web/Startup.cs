using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(ECommerceFX.Web.Startup))]

namespace ECommerceFX.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}