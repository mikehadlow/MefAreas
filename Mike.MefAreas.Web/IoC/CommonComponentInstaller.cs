using System.Reflection;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Mike.MefAreas.Core.Services;

namespace Mike.MefAreas.Web.IoC
{
    // this installer is installed for all addins.
    public class AddinInstaller : IWindsorInstaller
    {
        readonly Assembly assembly;

        public AddinInstaller(Assembly assembly)
        {
            this.assembly = assembly;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                    AllTypes
                        .FromAssembly(assembly)
                        .BasedOn<IController>()
                        .WithService.Base()
                        .Configure(c => c.LifeStyle.Transient.Named(c.Implementation.Name.ToLower())),
                    AllTypes
                        .FromAssembly(assembly)
                        .BasedOn<INavigation>()
                        .WithService.Base()
                        .Configure(c => c.LifeStyle.Transient.Named(c.Implementation.Name.ToLower()))
                );
        }
    }
}