using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Mike.MefAreas.AddIn.Controllers;
using Mike.MefAreas.AddIn.Install;
using Mike.MefAreas.Core.Services;

namespace Mike.MefAreas.AddIn.Installers
{
    public class CustomerAddinInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IController>().ImplementedBy<CustomerController>()
                    .Named("customercontroller").LifeStyle.Transient,
                Component.For<INavigation>().ImplementedBy<CustomerNavigation>()
                    .Named("customernavigation").LifeStyle.Transient
                );
        }
    }
}