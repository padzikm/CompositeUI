using System.IO;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using CompositeUI.Infrastructure;
using CompositeUI.Service.Infrastructure.Consts;
using NServiceBus;

namespace CompositeUI.Customers.Web.Windsor
{
    public class WindsorBootstraper : ServiceWindsorInstaller
    {
        private static volatile IWindsorContainer _container;
        private static volatile IBus _bus;
        private static readonly object _lockObject = new object();

        internal static IWindsorContainer Container { get { return _container; } }
        internal static IBus NBus { get { return _bus; } }

        public WindsorBootstraper(string binPath) : base(binPath) { }

        public override void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var path = GetProjectPath();

            ConfigureContainer(path);
            ConfigureNServiceBus();

            container.Install(new ExternalWindsorInstaller(path, _container));
        }

        private string GetProjectPath()
        {
            //string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            //UriBuilder uri = new UriBuilder(codeBase);
            //string uriPath = Uri.UnescapeDataString(uri.Path);
            //var dirPath = Path.GetDirectoryName(uriPath);
            //var path = Path.Combine(dirPath, NewsConsts.ServiceBinFolder);
            var path = Path.Combine(_binPath, CustomersConsts.ServiceBinFolder);
            return path;
        }

        private void ConfigureContainer(string path)
        {
            if (_container == null)
                lock (_lockObject)
                    if (_container == null)
                    {
                        var container = new WindsorContainer();
                        ConfigureUtils(container);
                        container.Install(
                            FromAssembly.InDirectory(
                                new AssemblyFilter(path).FilterByName(
                                    p => p.Name.StartsWith("CompositeUI") && !p.Name.StartsWith("CompositeUI.Customers.Web"))),
                            new InternalWindsorInstaller(path));
                        _container = container;
                    }
        }

        private void ConfigureUtils(IWindsorContainer container)
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));
            container.AddFacility<TypedFactoryFacility>();
        }

        private void ConfigureNServiceBus()
        {
            if (_bus == null)
                lock (_lockObject)
                    if (_bus == null)
                    {
                        var busConfiguration = new BusConfiguration();
                        busConfiguration.UsePersistence<InMemoryPersistence>();
                        busConfiguration.EndpointName("CompositeUI.Customers.Web");
                        busConfiguration.UseTransport<MsmqTransport>();
                        busConfiguration.EnableInstallers();
                        busConfiguration.UseContainer<WindsorBuilder>(p => p.ExistingContainer(_container));
                        var startupBus = Bus.Create(busConfiguration);
                        _bus = startupBus.Start();
                    }
        }
    }
}