using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BreakAway.DependencyInjection;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using ITCloud.Web.Routing;
using Studentum.Infrastructure.DependencyInjection;
//using DependencyResolver = Studentum.Infrastructure.Utilities.DependencyResolver;

namespace BreakAway
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private IWindsorContainer _container;
        protected void Application_Start()
        {
            BreakAwayApplicationStarter.StartApplication();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            RouteTable.Routes.DiscoverMvcControllerRoutes(typeof(MvcApplication).Assembly, null);
            _container = CreateContainer();
        }

        private static IWindsorContainer CreateContainer()
        {
            IWindsorContainer windsorContainer = new WindsorContainer();

            windsorContainer.Kernel.Resolver.AddSubResolver(new CollectionResolver(windsorContainer.Kernel));
            windsorContainer.Install(FromAssembly.This());
            windsorContainer.Install(new RepositoryInstaller(Classes.FromAssemblyInDirectory(new AssemblyFilter(AppDomain.CurrentDomain.RelativeSearchPath))));

            var controllerFactory = new WindsorControllerFactory(windsorContainer.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            return windsorContainer;
        }

        protected void Application_End()
        {
            _container.Dispose();
        }

      
    }
}
