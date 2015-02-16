using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BreakAway.Domain;
using Studentum.Infrastructure;
using Studentum.Infrastructure.Caching;
using Studentum.Infrastructure.Repository;

namespace BreakAway.Web
{
    public class BreakAwayApplicationStarter : ApplicationStarterBase
    {
        public BreakAwayApplicationStarter()
        {
            RegisterModule(new RepositoryStarterModule(new FluentRepositoryProviderFactory(new ModelMapperPackage()), new CacheRepositoryProviderFactory(new DomainCacheModelMappingPackage()), null, null, new FluentExtensionProvider()));

            ICacheUpdateInvoker updateInvoker = new SinkCacheUpdateInvoker();
            RegisterModule(new CacheStarterModule(updateInvoker, new DomainCacheLoaderPackage()));
        }
    }
}