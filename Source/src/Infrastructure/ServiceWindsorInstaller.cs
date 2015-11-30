using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace HomeManager.Infrastructure
{
    public abstract class ServiceWindsorInstaller : IWindsorInstaller
    {
        protected readonly string _binPath;

        public ServiceWindsorInstaller(string binPath)
        {
            _binPath = binPath;
        }

        public abstract void Install(IWindsorContainer container, IConfigurationStore store);
    }
}
