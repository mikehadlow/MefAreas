using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Mike.MefAreas.Web.IoC
{
    public class CoreComponentsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // add array resolver
            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel, true));

            container.Register(
                Component.For<IControllerFactory>().ImplementedBy<IocControllerFactory>().LifeStyle.Transient
                );
        }
    }
}