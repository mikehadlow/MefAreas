using Castle.Windsor;

namespace Mike.MefAreas.Core.Services
{
    public interface IAddinInstaller
    {
        void DoRegistration(IWindsorContainer container);
    }
}