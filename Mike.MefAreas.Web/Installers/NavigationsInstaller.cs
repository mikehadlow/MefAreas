using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Mike.MefAreas.Core.Services;

namespace Mike.MefAreas.Web.Installers
{
    public class NavigationsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes
                    .FromThisAssembly()
                    .BasedOn<INavigation>()
                    .WithService.Base()
                    .Configure(c => c.LifeStyle.Transient
                                        .Named(c.Implementation.Name.ToLower()))
                );
        }
    }
}