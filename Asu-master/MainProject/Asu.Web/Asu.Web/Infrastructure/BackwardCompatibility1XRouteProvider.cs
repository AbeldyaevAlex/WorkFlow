using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using Asu.Framework.Mvc.Routes;

namespace Asu.Web.Infrastructure
{
    //Routes used for backward compatibility with 1.x versions of nopCommerce
    public partial class BackwardCompatibility1XRouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            var supportPreviousNopcommerceVersions =
                !String.IsNullOrEmpty(ConfigurationManager.AppSettings["SupportPreviousNopcommerceVersions"]) &&
                Convert.ToBoolean(ConfigurationManager.AppSettings["SupportPreviousNopcommerceVersions"]);
            if (!supportPreviousNopcommerceVersions)
                return;

            //all old aspx URLs
            routes.MapRoute("", "{oldfilename}.aspx",
                            new { controller = "BackwardCompatibility1X", action = "GeneralRedirect" },
                            new[] { "Asu.Web.Controllers" });
            
            //products
            routes.MapRoute("", "products/{id}.aspx",
                            new { controller = "BackwardCompatibility1X", action = "RedirectProduct"},
                            new[] { "Asu.Web.Controllers" });
            
            //categories
            routes.MapRoute("", "category/{id}.aspx",
                            new { controller = "BackwardCompatibility1X", action = "RedirectCategory" },
                            new[] { "Asu.Web.Controllers" });

            //manufacturers
            routes.MapRoute("", "manufacturer/{id}.aspx",
                            new { controller = "BackwardCompatibility1X", action = "RedirectManufacturer" },
                            new[] { "Asu.Web.Controllers" });

            //product tags
            routes.MapRoute("", "producttag/{id}.aspx",
                            new { controller = "BackwardCompatibility1X", action = "RedirectProductTag" },
                            new[] { "Asu.Web.Controllers" });

            //news
            routes.MapRoute("", "news/{id}.aspx",
                            new { controller = "BackwardCompatibility1X", action = "RedirectNewsItem" },
                            new[] { "Asu.Web.Controllers" });

            //blog posts
            routes.MapRoute("", "blog/{id}.aspx",
                            new { controller = "BackwardCompatibility1X", action = "RedirectBlogPost" },
                            new[] { "Asu.Web.Controllers" });

            //topics
            routes.MapRoute("", "topic/{id}.aspx",
                            new { controller = "BackwardCompatibility1X", action = "RedirectTopic" },
                            new[] { "Asu.Web.Controllers" });

            //forums
            routes.MapRoute("", "boards/fg/{id}.aspx",
                            new { controller = "BackwardCompatibility1X", action = "RedirectForumGroup" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapRoute("", "boards/f/{id}.aspx",
                            new { controller = "BackwardCompatibility1X", action = "RedirectForum" },
                            new[] { "Asu.Web.Controllers" });
            routes.MapRoute("", "boards/t/{id}.aspx",
                            new { controller = "BackwardCompatibility1X", action = "RedirectForumTopic" },
                            new[] { "Asu.Web.Controllers" });
        }

        public int Priority
        {
            get
            {
                //register it after all other IRouteProvider are processed
                return -1000;
            }
        }
    }
}
