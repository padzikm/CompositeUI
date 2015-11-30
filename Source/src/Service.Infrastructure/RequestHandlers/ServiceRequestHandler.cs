using System.Web.Routing;
using HomeManager.Service.Infrastructure.Routing;

namespace HomeManager.Service.Infrastructure.RequestHandlers
{
	public class CustomersRequestHandler : RequestHandler
    {
        private static readonly RouteCollection _routes;

        static CustomersRequestHandler()
        {
            _routes = new RouteCollection();
            CustomersInternalRouteConfig.RegisterRoutes(_routes);
        }

        protected override RouteCollection Routes
        {
            get { return _routes; }
        }
    }

	public class OrdersRequestHandler : RequestHandler
    {
        private static readonly RouteCollection _routes;

        static OrdersRequestHandler()
        {
            _routes = new RouteCollection();
            OrdersInternalRouteConfig.RegisterRoutes(_routes);
        }

        protected override RouteCollection Routes
        {
            get { return _routes; }
        }
    }

	public class ProductsRequestHandler : RequestHandler
    {
        private static readonly RouteCollection _routes;

        static ProductsRequestHandler()
        {
            _routes = new RouteCollection();
            ProductsInternalRouteConfig.RegisterRoutes(_routes);
        }

        protected override RouteCollection Routes
        {
            get { return _routes; }
        }
    }
}