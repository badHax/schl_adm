using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SchoolAdmin.MVC.Startup))]
namespace SchoolAdmin.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
