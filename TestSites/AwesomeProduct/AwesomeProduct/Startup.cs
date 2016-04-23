using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AwesomeProduct.Startup))]
namespace AwesomeProduct
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
