using System.Web.Mvc;

namespace Asu.Web.Areas.msi
{
    public class msiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "msi";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "msi_default",
                "msi/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}