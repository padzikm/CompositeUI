using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace CompositeUI.Service.Infrastructure
{
	internal class CustomersInternalRouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
			foreach (var publicRoute in RouteTable.Routes)
            {
                var serviceRoute = publicRoute as ServiceRoute;
                if(serviceRoute != null)
                    continue;
                var webRoute = publicRoute as WebRoute;
                if(webRoute == null)
                    continue;
                var route = new Route(webRoute.Url, webRoute.RouteHandler)
				{
					RouteExistingFiles = webRoute.RouteExistingFiles,
					Constraints = new RouteValueDictionary(),
					DataTokens = new RouteValueDictionary(),
					Defaults = new RouteValueDictionary(),
				};
                foreach (var constraint in webRoute.Constraints)
                    route.Constraints[constraint.Key] = constraint.Value;
                foreach (var defaults in webRoute.Defaults)
                    route.Defaults[defaults.Key] = defaults.Value;
                foreach (var dataToken in webRoute.DataTokens)
                    route.DataTokens[dataToken.Key] = dataToken.Value;
				if(webRoute.DataTokens.ContainsKey("area"))
					route.DataTokens["Namespaces"] = new [] { "CompositeUI.Customers.Web.Areas." + webRoute.DataTokens["area"] + ".Controllers" };
				else
				{
					route.DataTokens["area"] = CustomersConsts.ServiceName;
					route.DataTokens["Namespaces"] = new [] { "CompositeUI.Customers.Web.Controllers" };
				}
                route.DataTokens[Consts.RouteServiceKey] = CustomersConsts.RouteServiceValue;
                route.DataTokens[Consts.RouteInternalServiceKey] = Consts.RouteInternalServiceValue;
				routes.Add(route);
            }
        }
    }

	internal class OrdersInternalRouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
			foreach (var publicRoute in RouteTable.Routes)
            {
                var serviceRoute = publicRoute as ServiceRoute;
                if(serviceRoute != null)
                    continue;
                var webRoute = publicRoute as WebRoute;
                if(webRoute == null)
                    continue;
                var route = new Route(webRoute.Url, webRoute.RouteHandler)
				{
					RouteExistingFiles = webRoute.RouteExistingFiles,
					Constraints = new RouteValueDictionary(),
					DataTokens = new RouteValueDictionary(),
					Defaults = new RouteValueDictionary(),
				};
                foreach (var constraint in webRoute.Constraints)
                    route.Constraints[constraint.Key] = constraint.Value;
                foreach (var defaults in webRoute.Defaults)
                    route.Defaults[defaults.Key] = defaults.Value;
                foreach (var dataToken in webRoute.DataTokens)
                    route.DataTokens[dataToken.Key] = dataToken.Value;
				if(webRoute.DataTokens.ContainsKey("area"))
					route.DataTokens["Namespaces"] = new [] { "CompositeUI.Orders.Web.Areas." + webRoute.DataTokens["area"] + ".Controllers" };
				else
				{
					route.DataTokens["area"] = OrdersConsts.ServiceName;
					route.DataTokens["Namespaces"] = new [] { "CompositeUI.Orders.Web.Controllers" };
				}
                route.DataTokens[Consts.RouteServiceKey] = OrdersConsts.RouteServiceValue;
                route.DataTokens[Consts.RouteInternalServiceKey] = Consts.RouteInternalServiceValue;
				routes.Add(route);
            }
        }
    }

	internal class ProductsInternalRouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
			foreach (var publicRoute in RouteTable.Routes)
            {
                var serviceRoute = publicRoute as ServiceRoute;
                if(serviceRoute != null)
                    continue;
                var webRoute = publicRoute as WebRoute;
                if(webRoute == null)
                    continue;
                var route = new Route(webRoute.Url, webRoute.RouteHandler)
				{
					RouteExistingFiles = webRoute.RouteExistingFiles,
					Constraints = new RouteValueDictionary(),
					DataTokens = new RouteValueDictionary(),
					Defaults = new RouteValueDictionary(),
				};
                foreach (var constraint in webRoute.Constraints)
                    route.Constraints[constraint.Key] = constraint.Value;
                foreach (var defaults in webRoute.Defaults)
                    route.Defaults[defaults.Key] = defaults.Value;
                foreach (var dataToken in webRoute.DataTokens)
                    route.DataTokens[dataToken.Key] = dataToken.Value;
				if(webRoute.DataTokens.ContainsKey("area"))
					route.DataTokens["Namespaces"] = new [] { "CompositeUI.Products.Web.Areas." + webRoute.DataTokens["area"] + ".Controllers" };
				else
				{
					route.DataTokens["area"] = ProductsConsts.ServiceName;
					route.DataTokens["Namespaces"] = new [] { "CompositeUI.Products.Web.Controllers" };
				}
                route.DataTokens[Consts.RouteServiceKey] = ProductsConsts.RouteServiceValue;
                route.DataTokens[Consts.RouteInternalServiceKey] = Consts.RouteInternalServiceValue;
				routes.Add(route);
            }
        }
    }
}
