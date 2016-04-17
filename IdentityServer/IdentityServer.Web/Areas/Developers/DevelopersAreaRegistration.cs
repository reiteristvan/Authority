using System.Web.Mvc;

namespace IdentityServer.Web.Areas.Developers
{
    public class DevelopersAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Developers";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Developers_default",
                "Developers/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "IdentityServer.Web.Areas.Developers.Controllers" }
            );
        }
    }
}