using System.Web.Routing;
using Asu.Framework.Localization;
using Asu.Framework.Mvc.Routes;
using Asu.Framework.Seo;

namespace Asu.Web.Infrastructure
{
    public partial class GenericUrlRouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            //generic URLs
            routes.MapGenericPathRoute("GenericUrl",
                                       "{generic_se_name}",
                                       new {controller = "Common", action = "GenericUrl"},
                                       new[] {"Asu.Web.Controllers"});

            //define this routes to use in UI views (in case if you want to customize some of them later)
            routes.MapLocalizedRoute("Product",
                                     "{SeName}",
                                     new { controller = "Product", action = "ProductDetails" },
                                     new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("Category",
                            "{SeName}",
                            new { controller = "Vehicle", action = "Category" },
                            new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("Manufacturer",
                            "{SeName}",
                            new { controller = "Vehicle", action = "Manufacturer" },
                            new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("Vendor",
                            "{SeName}",
                            new { controller = "Catalog", action = "Vendor" },
                            new[] { "Asu.Web.Controllers" });
            
            routes.MapLocalizedRoute("NewsItem",
                            "{SeName}",
                            new { controller = "News", action = "NewsItem" },
                            new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("BlogPost",
                            "{SeName}",
                            new { controller = "Blog", action = "BlogPost" },
                            new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("Topic",
                            "{SeName}",
                            new { controller = "Topic", action = "TopicDetails" },
                            new[] { "Asu.Web.Controllers" });


            #region WC

            routes.MapLocalizedRoute("CategoryManufacturer",
                            "{SeName}",
                            new { controller = "Vehicle", action = "CategoryManufacturer" },
                            new[] { "Asu.Web.Controllers" });

            routes.MapLocalizedRoute("ManufacturerCategory",
                            "{SeName}",
                            new { controller = "Vehicle", action = "ManufacturerCategory" },
                            new[] { "Asu.Web.Controllers" });

            #endregion
            //the last route. it's used when none of registered routes could be used for the current request
            //but it this case we cannot process non-registered routes (/controller/action)
            //routes.MapLocalizedRoute(
            //    "PageNotFound-Wildchar",
            //    "{*url}",
            //    new { controller = "Common", action = "PageNotFound" },
            //    new[] { "Nop.Web.Controllers" });
        }

        public int Priority
        {
            get
            {
                //it should be the last route
                //we do not set it to -int.MaxValue so it could be overriden (if required)
                return -1000000;
            }
        }
    }
}
