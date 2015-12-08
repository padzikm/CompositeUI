using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using CompositeUI.Infrastructure;

namespace CompositeUI.Web.Infrastructure
{
    public static class WebRouteRegistration
    {
        public static Route CreateRoute(this RouteCollection routes, string name, string url)
        {
            return CreateRoute(routes, name, url, (object)null);
        }

        public static Route CreateRoute(this RouteCollection routes, string name, string url, object defaults)
        {
            return CreateRoute(routes, name, url, defaults, (object)null);
        }

        public static Route CreateRoute(this RouteCollection routes, string name, string url, object defaults, object constraints)
        {
            return CreateRoute(routes, name, url, defaults, constraints, (string[])null);
        }

        public static Route CreateRoute(this RouteCollection routes, string name, string url, string[] namespaces)
        {
            return CreateRoute(routes, name, url, (object)null, namespaces);
        }

        public static Route CreateRoute(this RouteCollection routes, string name, string url, object defaults, string[] namespaces)
        {
            return CreateRoute(routes, name, url, defaults, (object)null, namespaces);
        }

        public static Route CreateRoute(this RouteCollection routes, string name, string url, object defaults, object constraints, string[] namespaces)
        {
            if (routes == null)
                throw new ArgumentNullException("routes");
            if (url == null)
                throw new ArgumentNullException("url");
            var route = new WebRoute(url, (IRouteHandler)new MvcRouteHandler())
            {
                Defaults = CreateRouteValueDictionary(defaults),//RouteCollectionExtensions.CreateRouteValueDictionaryUncached(defaults),
                Constraints = CreateRouteValueDictionary(constraints),//RouteCollectionExtensions.CreateRouteValueDictionaryUncached(constraints),
                DataTokens = new RouteValueDictionary()
            };
            if (namespaces != null && namespaces.Length > 0)
                route.DataTokens["Namespaces"] = (object)namespaces;
            Validate(route);
            routes.Add(name, (RouteBase)route);
            return route;
        }

        private static RouteValueDictionary CreateRouteValueDictionary(object values)
        {
            IDictionary<string, object> dictionary = values as IDictionary<string, object>;
            if (dictionary != null)
                return new RouteValueDictionary(dictionary);
            return TypeHelper.ObjectToDictionaryUncached(values);
        }

        private static void Validate(Route route)
        {
            if (route.Constraints == null)
                return;
            foreach (KeyValuePair<string, object> keyValuePair in route.Constraints)
            {
                if (!(keyValuePair.Value is string) && !(keyValuePair.Value is IRouteConstraint))
                    throw new InvalidOperationException(string.Format("Route constraints: {0}, {1}, {2}", (object)keyValuePair.Key, (object)route.Url, (object)typeof(IRouteConstraint).FullName));
            }
        }
    }
}