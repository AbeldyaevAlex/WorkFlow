using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Asu.Core.Infrastructure;
using Asu.Services.Seo;

namespace Asu.Framework.Seo
{
    using Asu.Core;
    using Asu.Core.Domain;

    public class VehicleSeoRoute : Route
    {
        public VehicleSeoRoute()
            : base("veh/{SeName}",
            new RouteValueDictionary(new { controller = "Vehicle", action = "Accessories" }),
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
                var slug = routeData.Values["SeName"] as string;
                var isVehicleSupported = EngineContext.Current.Resolve<StoreInformationSettings>().VehicleSupported;
                if (!isVehicleSupported)
                {
                    routeData.Values["controller"] = "Common";
                    routeData.Values["action"] = "PageNotFound";
                    return routeData;
                }

                var urlRecord = urlRecordService.GetVehicleBySlugCached(slug);
                if (urlRecord == null)
                {
                    var urlRecordRedirect = urlRecordService.GetVehicleRedirectBySlug(slug);
                    if (urlRecordRedirect != null)
                    {
                        var response = httpContext.Response;
                        var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                        response.Status = "301 Moved Permanently";
                        response.StatusCode = 301;
                        response.RedirectLocation = $"{webHelper.GetStoreLocation(true)}veh/{urlRecordRedirect.NewSlug}"; 
                        response.End();
                        return null;
                    }
                }

                if (urlRecord == null)
                {
                    routeData.Values["controller"] = "Common";
                    routeData.Values["action"] = "PageNotFound";
                    return routeData;
                }

                //process URL
                switch (urlRecord.EntityName.ToLowerInvariant())
                {
                    case "accessories":
                        {
                            routeData.Values["controller"] = "Vehicle";
                            routeData.Values["action"] = "VehicleAccessories";
                            routeData.Values["makeId"] = urlRecord.MakeId;
                            routeData.Values["modelId"] = urlRecord.ModelId;
                            routeData.Values["yearId"] = urlRecord.YearId;
                        }
                        break;
                    case "category":
                        {
                            routeData.Values["controller"] = "Vehicle";
                            routeData.Values["action"] = "VehicleCategory";
                            routeData.Values["categoryId"] = urlRecord.EntityId;
                            routeData.Values["makeId"] = urlRecord.MakeId;
                            routeData.Values["modelId"] = urlRecord.ModelId;
                            routeData.Values["yearId"] = urlRecord.YearId;
                        }
                        break;
                }
            }
            return routeData; 
        }
    }
}
