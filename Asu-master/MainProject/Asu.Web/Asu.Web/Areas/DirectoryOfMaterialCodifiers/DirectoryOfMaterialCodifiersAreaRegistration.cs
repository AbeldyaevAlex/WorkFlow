using System.Web.Mvc;

namespace Asu.Web.Areas.DirectoryOfMaterialCodifiers
{
    public class DirectoryOfMaterialCodifiersAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DirectoryOfMaterialCodifiers";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DirectoryOfMaterialCodifiers_default",
                "DirectoryOfMaterialCodifiers/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}