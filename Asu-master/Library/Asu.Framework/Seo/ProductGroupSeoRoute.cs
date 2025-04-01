namespace Asu.Framework.Seo
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Core.Infrastructure;

    using Asu.Core.Domain;
    using Asu.Core.Domain.Seo;

    using Asu.Services.Seo;

    public class ProductGroupSeoRoute : Route
    {
        public ProductGroupSeoRoute()
            : base("g/{ParentEntitySlug}/{EntitySlug}",
            new RouteValueDictionary(new { controller = "ProductGroup", action = "Details", EntitySlug = UrlParameter.Optional }),
            new RouteValueDictionary(),
            new RouteValueDictionary(new { namespaces = new[] { "Namespaces", "Asu.Web.Controllers" } }), 
            new MvcRouteHandler())
        {
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var routeData = base.GetRouteData(httpContext);
            if (routeData != null)
            {
                var urlRecordService = EngineContext.Current.Resolve<IUrlRecordService>();
                var parentEntitySlug = routeData.Values["ParentEntitySlug"] as string;
                var slug = routeData.Values["EntitySlug"] as string;
                var isProductGroupSupported = EngineContext.Current.Resolve<StoreInformationSettings>().VehicleSupported;
                if (!isProductGroupSupported)
                {
                    routeData.Values["controller"] = "Common";
                    routeData.Values["action"] = "PageNotFound";
                    return routeData;
                }

                var urlRecord = slug == null ? urlRecordService.GetProductGroupBySlug(parentEntitySlug) :  urlRecordService.GetProductGroupBySlug(parentEntitySlug, slug);
                
                if (urlRecord == null)
                {
                    routeData.Values["controller"] = "Common";
                    routeData.Values["action"] = "PageNotFound";
                    return routeData;
                }

                switch (urlRecord.EntityType)
                {
                    case GroupEntityType.ProductGroup:
                        routeData.Values["controller"] = "ProductGroup";
                        routeData.Values["action"] = "Details";
                        routeData.Values["productGroupId"] = urlRecord.EntityId;
                        break;
                    case GroupEntityType.Brand:
                        routeData.Values["controller"] = "ProductGroup";
                        routeData.Values["action"] = "Brand";
                        routeData.Values["brandId"] = urlRecord.EntityId;
                        break;
                    case GroupEntityType.BrandCategory:
                        routeData.Values["controller"] = "ProductGroup";
                        routeData.Values["action"] = "Category";
                        routeData.Values["categoryId"] = urlRecord.EntityId;
                        break;
                }
            }

            return routeData; 
        }
    }
}
