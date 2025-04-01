using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Asu.Web {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");
            routes.MapRoute(
                "Default", 
                "{controller}/{action}/{id}",
                new {  controller = "ImageSlider", action = "FullScreen", id = UrlParameter.Optional },
                namespaces: new[] {"Asu.Web.Controllers"}
            );
        }
    }
}