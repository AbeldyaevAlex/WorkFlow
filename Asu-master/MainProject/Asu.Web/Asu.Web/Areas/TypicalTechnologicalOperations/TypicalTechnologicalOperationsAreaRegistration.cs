using System.Web.Mvc;

namespace Asu.Web.Areas.TypicalTechnologicalOperations
{
    public class TypicalTechnologicalOperationsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TypicalTechnologicalOperations";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "TypicalTechnologicalOperations_default",
                "TypicalTechnologicalOperations/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}