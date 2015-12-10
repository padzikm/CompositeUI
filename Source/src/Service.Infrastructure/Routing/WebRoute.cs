using System.Web.Routing;

namespace CompositeUI.Service.Infrastructure
{
    public class WebRoute : Route
    {
        public WebRoute(string url, IRouteHandler routeHandler) : base(url, routeHandler)
        {
        }

        public WebRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler) : base(url, defaults, routeHandler)
        {
        }

        public WebRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler) : base(url, defaults, constraints, routeHandler)
        {
        }

        public WebRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler) : base(url, defaults, constraints, dataTokens, routeHandler)
        {
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            if ((values.ContainsKey(Consts.RouteServiceKey) || requestContext.RouteData.DataTokens.ContainsKey(Consts.RouteServiceKey)) && !values.ContainsKey(Consts.RouteExternalService))
                return null;

            var tmpValues = new RouteValueDictionary(values);
            tmpValues.Remove(Consts.RouteServiceKey);
            tmpValues.Remove(Consts.RouteExternalService);

            return base.GetVirtualPath(requestContext, tmpValues);
        }
    }
}