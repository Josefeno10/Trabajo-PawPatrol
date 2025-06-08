using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PedidosComida.Startup))]
namespace PedidosComida
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
