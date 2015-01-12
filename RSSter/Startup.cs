using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RSSter.Startup))]
namespace RSSter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
