using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace CompositeUI.Service.Infrastructure
{
    public abstract class WebAreaRegistration : AreaRegistration
    {
        protected Route CreateRoute(AreaRegistrationContext context, string name, string url)
        {
            return CreateRoute(context, name, url, (object)null);
        }
        protected Route CreateRoute(AreaRegistrationContext context, string name, string url, object defaults)
        {
            return CreateRoute(context, name, url, defaults, (object)null);
        }

        protected Route CreateRoute(AreaRegistrationContext context, string name, string url, object defaults, object constraints)
        {
            return CreateRoute(context, name, url, defaults, constraints, (string[])null);
        }

        protected Route CreateRoute(AreaRegistrationContext context, string name, string url, string[] namespaces)
        {
            return CreateRoute(context, name, url, (object)null, namespaces);
        }

        protected Route CreateRoute(AreaRegistrationContext context, string name, string url, object defaults, string[] namespaces)
        {
            return CreateRoute(context, name, url, defaults, (object)null, namespaces);
        }

        protected Route CreateRoute(AreaRegistrationContext context, string name, string url, object defaults, object constraints, string[] namespaces)
        {
            if (namespaces == null && context.Namespaces != null)
                namespaces = Enumerable.ToArray<string>((IEnumerable<string>)context.Namespaces);
            var route = CreateUrlRoute(context.Routes, name, url, defaults, constraints, namespaces);
            route.DataTokens["area"] = (object)this.AreaName;
            bool flag = namespaces == null || namespaces.Length == 0;
            route.DataTokens["UseNamespaceFallback"] = (object)flag;
            return route;
        }

        private Route CreateUrlRoute(RouteCollection routes, string name, string url, object defaults, object constraints, string[] namespaces)
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

        private RouteValueDictionary CreateRouteValueDictionary(object values)
        {
            IDictionary<string, object> dictionary = values as IDictionary<string, object>;
            if (dictionary != null)
                return new RouteValueDictionary(dictionary);
            return TypeHelper.ObjectToDictionaryUncached(values);
        }

        private void Validate(Route route)
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