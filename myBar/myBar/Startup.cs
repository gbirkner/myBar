using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(myBar.Startup))]
namespace myBar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
