using System.Web.Mvc;

namespace Asu.Web.Areas.mmn
{
    public class mmnAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "mmn";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "mmn_default",
                "mmn/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Asu.Web.Areas.mmn.Controllers" }
            );
        }
    }
}