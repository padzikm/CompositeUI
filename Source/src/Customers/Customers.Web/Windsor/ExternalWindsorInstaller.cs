using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CompositeUI.Infrastructure;
using CompositeUI.Service.Infrastructure.Controllers;
using CompositeUI.Service.Infrastructure.RequestHandlers;
using CompositeUI.Service.Infrastructure.ViewEngines;

namespace CompositeUI.Customers.Web.Windsor
{
    internal class ExternalWindsorInstaller : IWindsorInstaller
    {
        private readonly string _path;
        private readonly IWindsorContainer _internalContainer;

        public ExternalWindsorInstaller(string path, IWindsorContainer internalContainer)
        {
            _path = path;
            _internalContainer = internalContainer;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IControllerActivator>().UsingFactoryMethod(() => new WindsorControllerActivator(_internalContainer.Kernel)).Named("CustomersActivator"));
            container.Register(Component.For<IControllerFactory>().UsingFactoryMethod(() => new WindsorControllerFactory(_internalContainer.Kernel)).Named("CustomersFactory"));
            container.Register(Component.For<IRequestHandler>().UsingFactoryMethod(() => new CustomersRequestHandler()).Named("CustomersRequestHandler"));
            container.Register(Component.For<IViewEngine>().UsingFactoryMethod(() => new CustomersViewEngine()).Named("CustomersViewEngine"));
            container.Register(Classes.FromThisAssembly().BasedOn<IApplicationConfiguration>().WithServiceBase());
        }
    }
}