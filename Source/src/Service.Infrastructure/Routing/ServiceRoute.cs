using System;
using System.Web;
using System.Web.Routing;

namespace CompositeUI.Service.Infrastructure
{
    public class ServiceRoute : Route
    {
        public ServiceRoute(string url, IRouteHandler routeHandler) : base(url, routeHandler)
        {
        }

        public ServiceRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler) : base(url, defaults, routeHandler)
        {
        }

        public ServiceRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler) : base(url, defaults, constraints, routeHandler)
        {
        }

        public ServiceRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler) : base(url, defaults, constraints, dataTokens, routeHandler)
        {
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (!DataTokens.ContainsKey(Consts.RouteServiceKey))
                throw new InvalidOperationException("DataTokens must contain RouteServiceKey in service routing");

            return base.GetRouteData(httpContext);
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            if (!DataTokens.ContainsKey(Consts.RouteServiceKey))
                throw new InvalidOperationException("DataTokens must contain RouteServiceKey in service routing");
            if (!values.ContainsKey(Consts.RouteServiceKey) && !requestContext.RouteData.DataTokens.ContainsKey(Consts.RouteServiceKey))
                return null;
            if (values.ContainsKey(Consts.RouteExternalService))
                return null;
            if (DataTokens[Consts.RouteServiceKey] != values[Consts.RouteServiceKey] && DataTokens[Consts.RouteServiceKey] != requestContext.RouteData.DataTokens[Consts.RouteServiceKey])
                return null;

            var tmpValues = new RouteValueDictionary(values);
            tmpValues.Remove(Consts.RouteServiceKey);

            return base.GetVirtualPath(requestContext, tmpValues);
        }
    }
}
