namespace Asu.Web.Infrastructure
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Asu.Framework.Mvc.Routes;

    public class ThmCompatibilityRouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            var pattern = @"\d{1,9}";

            routes.MapRoute(string.Empty, "i-{id}.aspx",
                new { controller = "ThmCompatibility", action = "RedirectProduct" },
                namespaces: new[] { "Asu.Web.Controllers" },
                constraints: new { id = pattern });

            routes.MapRoute(string.Empty, "{segment0}/i-{id}.aspx",
                new { controller = "ThmCompatibility", action = "RedirectProduct" },
                namespaces: new[] { "Asu.Web.Controllers" },
                constraints: new { id = pattern });

            routes.MapRoute(string.Empty, "{segment0}/{segment1}/{segment2}/i-{id}.aspx",
                new { controller = "ThmCompatibility", action = "RedirectProduct" },
                namespaces: new[] { "Asu.Web.Controllers" },
                constraints: new { id = pattern });

            routes.MapRoute(string.Empty, "{segment0}/{segment1}/{segment2}/{segment3}/i-{id}.aspx",
                new { controller = "ThmCompatibility", action = "RedirectProduct" },
                namespaces: new[] { "Asu.Web.Controllers" },
                constraints: new { id = pattern });

            routes.MapRoute(string.Empty, "p-{id}.aspx",
                new { controller = "ThmCompatibility", action = "RedirectCategory" },
                namespaces: new[] { "Asu.Web.Controllers" },
                constraints: new { id = pattern });

            routes.MapRoute(string.Empty, "{segment0}/p-{id}.aspx",
                new { controller = "ThmCompatibility", action = "RedirectCategory" },
                namespaces: new[] { "Asu.Web.Controllers" },
                constraints: new { id = pattern });

            routes.MapRoute(string.Empty, "b-{id}.aspx",
                new { controller = "ThmCompatibility", action = "RedirectManufacturer" },
                namespaces: new[] { "Asu.Web.Controllers" },
                constraints: new { id = pattern });

            routes.MapRoute(string.Empty, "{segment0}/b-{id}.aspx",
                new { controller = "ThmCompatibility", action = "RedirectManufacturer" },
                namespaces: new[] { "Asu.Web.Controllers" },
                constraints: new { id = pattern });

            routes.MapRoute(string.Empty, "store/product/search.aspx",
                new { controller = "ThmCompatibility", action = "RedirectSearch" },
                new[] { "Asu.Web.Controllers" });
        }

        public int Priority => 1;
    }
}