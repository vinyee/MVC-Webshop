using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DemoWithUsers.Startup))]
namespace DemoWithUsers
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
