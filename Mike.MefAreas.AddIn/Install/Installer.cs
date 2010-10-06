using System.ComponentModel.Composition;
using Castle.Windsor;
using Mike.MefAreas.Core.Services;

namespace Mike.MefAreas.AddIn.Install
{
    [Export(typeof(IAddinInstaller)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class Installer : IAddinInstaller
    {
        public void DoRegistration(IWindsorContainer container)
        {
            // do any additional registration
        }
    }
}