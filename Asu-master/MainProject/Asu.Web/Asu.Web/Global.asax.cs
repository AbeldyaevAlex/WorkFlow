using Asu.Core.Data;
using Asu.Core.Infrastructure;
using Asu.Services.Logging;
using Asu.Web.App_Start;
using Asu.Web.AutoMapperConfig;
using Asu.Framework.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Asu.Framework.Mvc.Routes;
using System.Net;
using Asu.Framework.Mvc;
using Asu.Framework;
using FluentValidation.Mvc;
using Asu.Services.Tasks;
using Asu.Core;
using Asu.Web.Controllers;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using Asu.Core.Domain.Common;
using static DevExpress.Web.Internal.CodeFormatter;

namespace Asu.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //register custom routes (plugins, etc)
            var routePublisher = EngineContext.Current.Resolve<IRoutePublisher>();
            routePublisher.RegisterRoutes(routes);

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                                              //new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new { controller = "ImageSlider", action = "FullScreen", id = UrlParameter.Optional },
                new[] { "Asu.Web.Controllers" }
            );

            routes.MapRoute(
                name: "DefaultWebApi",
                url: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }


        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            EngineContext.Initialize(false);

            bool databaseInstalled = DataSettingsHelper.DatabaseIsInstalled();
            if (databaseInstalled)
            {
                //remove all view engines
                ViewEngines.Engines.Clear();
                //except the themeable razor view engine we use
                ViewEngines.Engines.Add(new ThemeableRazorViewEngine());
            }

            //Add some functionality on top of the default ModelMetadataProvider
            ModelMetadataProviders.Current = new NopMetadataProvider();

            //Registering some regular mvc stuff
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //fluent validation
            //DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            //ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(new NopValidatorFactory()));
            /*FluentValidationModelValidatorProvider.Configure(x =>
            {
                x.Add(typeof(ReturnRequestItemValidator), (metadata, Context, rule, validator) => new ReturnReasonSelectedPropertyValidator(metadata, Context, rule, validator));
            });*/

            //start scheduled tasks
            if (databaseInstalled)
            {
                TaskManager.Instance.Initialize();
                TaskManager.Instance.Start();
            }

            //log application start
            if (databaseInstalled)
            {
                try
                {
                    //log
                    var logger = EngineContext.Current.Resolve<ILogger>();
                    logger.Information($"Application started", null, null);
                }
                catch (Exception)
                {
                    //don't throw new exception if occurs
                }
            }




            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            ModelBinders.Binders.DefaultBinder = new DevExpress.Web.Mvc.DevExpressEditorsBinder();
            DevExpress.Web.ASPxWebControl.CallbackError += Application_Error;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //ignore static resources
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            if (webHelper.IsStaticResource(this.Request))
                return;

            //keep alive page requested (we ignore it to prevent creating a guest customer records)
            string keepAliveUrl = $"{webHelper.GetStoreLocation()}keepalive/index";
            if (webHelper.GetThisPageUrl(false).StartsWith(keepAliveUrl, StringComparison.InvariantCultureIgnoreCase))
                return;

            // HACK: check if Url contains SessionId (cookieless Url), If so the redirect to Url without SessionId
            var sessonId = this.Response.ApplyAppPathModifier(string.Empty);
            if (!string.IsNullOrEmpty(sessonId))
            {
                if (Regex.IsMatch(sessonId, @"\/\(S\(([a-zA-Z_0-9()]+)\)\)\/"))
                {
                    var redurectUrl = this.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.RawUrl;
                    HttpContext.Current.Response.RedirectPermanent(redurectUrl);
                }
            }

            //ensure database is installed
            /*if (!DataSettingsHelper.DatabaseIsInstalled())
            {
                string installUrl = string.Format("{0}install", webHelper.GetStoreLocation());
                if (!webHelper.GetThisPageUrl(false).StartsWith(installUrl, StringComparison.InvariantCultureIgnoreCase))
                {
                    this.Response.Redirect(installUrl);
                }
            }

            if (!DataSettingsHelper.DatabaseIsInstalled())
                return;*/

        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            //we don't do it in Application_BeginRequest because a user is not authenticated yet
            this.SetWorkingCulture();
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            var exception = this.Server.GetLastError();

            //log error
            this.LogException(exception);

            //process 404 HTTP errors
            var httpException = exception as HttpException;
            if (httpException != null && httpException.GetHttpCode() == 404)
            {
                var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                if (!webHelper.IsStaticResource(this.Request))
                {
                    this.Response.Clear();
                    this.Server.ClearError();
                    this.Response.TrySkipIisCustomErrors = true;

                    // Call target Controller and pass the routeData.
                    IController errorController = EngineContext.Current.Resolve<CommonController>();

                    var routeData = new RouteData();
                    routeData.Values.Add("controller", "Common");
                    routeData.Values.Add("action", "PageNotFound");

                    errorController.Execute(new RequestContext(new HttpContextWrapper(this.Context), routeData));
                }
            }
        }

        protected void SetWorkingCulture()
        {
            if (!DataSettingsHelper.DatabaseIsInstalled())
                return;

            //ignore static resources
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            if (webHelper.IsStaticResource(this.Request))
                return;

            //keep alive page requested (we ignore it to prevent creation of guest customer records)
            string keepAliveUrl = $"{webHelper.GetStoreLocation()}keepalive/index";
            if (webHelper.GetThisPageUrl(false).StartsWith(keepAliveUrl, StringComparison.InvariantCultureIgnoreCase))
                return;


            if (webHelper.GetThisPageUrl(false).StartsWith($"{webHelper.GetStoreLocation()}admin",
                StringComparison.InvariantCultureIgnoreCase))
            {
                //admin area


                //always set culture to 'en-US'
                //we set culture of admin area to 'en-US' because current implementation of Telerik grid 
                //doesn't work well in other cultures
                //e.g., editing decimal value in russian culture
                CommonHelper.SetTelerikCulture();
            }
            else
            {
                //public store
                var workContext = EngineContext.Current.Resolve<IWorkContext>();
                var culture = new CultureInfo(workContext.WorkingLanguage.LanguageCulture);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
        }

        protected void LogException(Exception exc)
        {
            if (exc == null)
                return;

            if (!DataSettingsHelper.DatabaseIsInstalled())
                return;

            //ignore 404 HTTP errors
            var httpException = exc as HttpException;
            if (httpException != null && httpException.GetHttpCode() == 404 &&
                !EngineContext.Current.Resolve<CommonSettings>().Log404Errors)
                return;

            try
            {
                //log
                var logger = EngineContext.Current.Resolve<ILogger>();
                var workContext = EngineContext.Current.Resolve<IWorkContext>();
                logger.Error(exc.Message, exc, workContext.CurrentCustomer);
            }
            catch (Exception)
            {
                //don't throw new exception if occurs
            }
        }
    }
}
