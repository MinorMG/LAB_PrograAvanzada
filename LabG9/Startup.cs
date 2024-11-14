using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LabG9.Startup))]
namespace LabG9
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
