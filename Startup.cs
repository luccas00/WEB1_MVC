using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LuccasCorp.Startup))]
namespace LuccasCorp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
