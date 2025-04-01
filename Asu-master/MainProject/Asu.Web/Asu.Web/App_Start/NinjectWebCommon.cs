[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Asu.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Asu.Web.App_Start.NinjectWebCommon), "Stop")]

namespace Asu.Web.App_Start
{
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicValidationHelper;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Repository;
    using Services;
    using Asu.Core.Data;
    using Asu.Core;
    using Asu.Data;
    using Asu.Services.CustomerServices;
    using Asu.Services.Logging;
    using Asu.Services.Workshop;
    using Asu.Services;
    using Asu.Core.CustomerAsu;
    using Asu.Framework;

    public class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();
        public static void Start()
        {
            //DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            //DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<Asu.Web.Repository.IRepository<Models.Msi.Spr_cex>>().To<SprCexRepository>();
            kernel.Bind<SprCexService>().ToSelf();

            kernel.Bind<IWoksopRepository<Core.Domain.Msi.Spr_cex>>().To<WoksopRepository>();
            kernel.Bind<WorkshopService>().ToSelf();

            kernel.Bind<ICustomerRepository<ApplicationUser>>().To<CustomerRepository>();
            kernel.Bind<CustomerServices>().ToSelf();

            //kernel.Bind<IWorkContext>().To<WebWorkContext>();

            var dataSettingsManager = new DataSettingsManager();
            var dataProviderSettings = dataSettingsManager.LoadSettings();
            //kernel.Bind<IDbContext>().ToMethod(c => new NopObjectContext(dataProviderSettings.DataConnectionString));

            kernel.Bind<ILogger>().To<DefaultLogger>();

            kernel.Bind(typeof(Core.Data.IRepository<>)).To(typeof(EfRepository<>));

            //kernel.Bind<IWebHelper>().To<WebHelper>();
        }
    }
}