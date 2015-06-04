using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RemoteControl.Startup))]
namespace RemoteControl
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
