using BreakAway.Services;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BreakAway.Installers
{
    public class FilterInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IFilterService>().ImplementedBy<FilterService>().LifeStyle.Singleton);
            //container.Register(Component.For<IContactFilter>().ImplementedBy<FirstNameContactFilter>().LifeStyle.Singleton);
            //container.Register(Component.For<IContactFilter>().ImplementedBy<LastNameContactFilter>().LifeStyle.Singleton);
            container.Register(Classes.FromAssemblyInThisApplication().BasedOn<IContactFilter>().WithServiceAllInterfaces().LifestyleSingleton());
        }
    }
}