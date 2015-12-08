using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CompositeUI.Infrastructure;
using CompositeUI.Service.Infrastructure.Controllers;
using CompositeUI.Service.Infrastructure.RequestHandlers;
using CompositeUI.Service.Infrastructure.ViewEngines;

namespace CompositeUI.Orders.Web.Windsor
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
            container.Register(Component.For<IControllerActivator>().UsingFactoryMethod(() => new WindsorControllerActivator(_internalContainer.Kernel)).Named("OrdersActivator"));
            container.Register(Component.For<IControllerFactory>().UsingFactoryMethod(() => new WindsorControllerFactory(_internalContainer.Kernel)).Named("OrdersFactory"));
            container.Register(Component.For<IRequestHandler>().UsingFactoryMethod(() => new OrdersRequestHandler()).Named("OrdersRequestHandler"));
            container.Register(Component.For<IViewEngine>().UsingFactoryMethod(() => new OrdersViewEngine()).Named("OrdersViewEngine"));
            container.Register(Classes.FromThisAssembly().BasedOn<IApplicationConfiguration>().WithServiceBase());
        }
    }
}