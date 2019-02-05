using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BreakAway.Domain;
using Studentum.Infrastructure;
using Studentum.Infrastructure.Caching;
using Studentum.Infrastructure.Repository;

namespace BreakAway
{
    public class BreakAwayApplicationStarter : ApplicationStarterBase
    {
        private static ApplicationStarterBase _applicationStarter;
        public BreakAwayApplicationStarter()
        {
            this.RegisterModule(new RepositoryStarterModule(
                    new FluentRepositoryProviderFactory(new CoreFluentMappers()),
                    null,null, null, new FluentExtensionProvider()));

        }

        public static void StartApplication()
        {
            if (_applicationStarter != null)
            {
                throw new InvalidOperationException("Application already running");
            }

            _applicationStarter = new BreakAwayApplicationStarter();

            _applicationStarter.Start();
        }
    }
}