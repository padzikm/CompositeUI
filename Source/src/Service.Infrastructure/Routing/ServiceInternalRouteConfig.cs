using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using CompositeUI.Service.Infrastructure.Consts;

namespace CompositeUI.Service.Infrastructure.Routing
{
	internal class CustomersInternalRouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            foreach (var route in RegisteredRoutes(routes))
            {
                route.DataTokens[Consts.Consts.RouteServiceKey] = CustomersConsts.RouteServiceValue;
                route.DataTokens[Consts.Consts.RouteInternalServiceKey] = Consts.Consts.RouteInternalServiceValue;
            }
        }

        private static IEnumerable<Route> RegisteredRoutes(RouteCollection routes)
        {
            var route = routes.MapRoute(
                "TestArea_default",
                "TestArea/{controller}/{action}/{id}",
                new {action = "Index", id = UrlParameter.Optional},
                new[] {"CompositeUI.Customers.Web.Areas.TestArea.Controllers"}
                );
            route.DataTokens["area"] = "TestArea";
            yield return route;

            route = routes.MapRoute(
                name: "Default",
                namespaces: new[] { "CompositeUI.Customers.Web.Controllers" },
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            route.DataTokens["area"] = CustomersConsts.ServiceName;
            yield return route;
        }
    }

	internal class OrdersInternalRouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            foreach (var route in RegisteredRoutes(routes))
            {
                route.DataTokens[Consts.Consts.RouteServiceKey] = OrdersConsts.RouteServiceValue;
                route.DataTokens[Consts.Consts.RouteInternalServiceKey] = Consts.Consts.RouteInternalServiceValue;
            }
        }

        private static IEnumerable<Route> RegisteredRoutes(RouteCollection routes)
        {
            var route = routes.MapRoute(
                "TestArea_default",
                "TestArea/{controller}/{action}/{id}",
                new {action = "Index", id = UrlParameter.Optional},
                new[] {"CompositeUI.Orders.Web.Areas.TestArea.Controllers"}
                );
            route.DataTokens["area"] = "TestArea";
            yield return route;

            route = routes.MapRoute(
                name: "Default",
                namespaces: new[] { "CompositeUI.Orders.Web.Controllers" },
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            route.DataTokens["area"] = OrdersConsts.ServiceName;
            yield return route;
        }
    }

	internal class ProductsInternalRouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            foreach (var route in RegisteredRoutes(routes))
            {
                route.DataTokens[Consts.Consts.RouteServiceKey] = ProductsConsts.RouteServiceValue;
                route.DataTokens[Consts.Consts.RouteInternalServiceKey] = Consts.Consts.RouteInternalServiceValue;
            }
        }

        private static IEnumerable<Route> RegisteredRoutes(RouteCollection routes)
        {
            var route = routes.MapRoute(
                "TestArea_default",
                "TestArea/{controller}/{action}/{id}",
                new {action = "Index", id = UrlParameter.Optional},
                new[] {"CompositeUI.Products.Web.Areas.TestArea.Controllers"}
                );
            route.DataTokens["area"] = "TestArea";
            yield return route;

            route = routes.MapRoute(
                name: "Default",
                namespaces: new[] { "CompositeUI.Products.Web.Controllers" },
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            route.DataTokens["area"] = ProductsConsts.ServiceName;
            yield return route;
        }
    }
}
