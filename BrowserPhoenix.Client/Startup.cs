using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BrowserPhoenix.Startup))]
namespace BrowserPhoenix
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
