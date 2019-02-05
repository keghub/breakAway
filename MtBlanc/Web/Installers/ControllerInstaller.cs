using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace BreakAway.Installers
{
    public class ControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyInThisApplication()
                .BasedOn<IController>()
                .Configure(r =>
                {
                    r.Named(string.Format("{0} (Default)", r.Implementation.FullName));
                    r.IsFallback();
                }).LifestyleTransient());
        }
    }
}