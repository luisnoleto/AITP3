using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AITP3.Startup))]
namespace AITP3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
