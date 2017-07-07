using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bulka.Startup))]
namespace Bulka
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
