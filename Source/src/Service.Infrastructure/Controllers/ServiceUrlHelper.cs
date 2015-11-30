using System.Web.Mvc;
using System.Web.Routing;
using HomeManager.Infrastructure;

namespace HomeManager.Service.Infrastructure.Controllers
{
    public class ServiceUrlHelper : UrlHelper
    {
        public ServiceUrlHelper(RequestContext context) : base(context)
        {
        }

        public override string Action(string actionName)
        {
            var routeValues = new RouteValueDictionary();
            if (RequestContext.RouteData.DataTokens.ContainsKey(Consts.Consts.RouteServiceKey))
                routeValues[Consts.Consts.RouteServiceKey] = RequestContext.RouteData.DataTokens[Consts.Consts.RouteServiceKey];
            return base.Action(actionName, routeValues);
        }

        public override string Action(string actionName, object routeValues)
        {
            var dict = TypeHelper.ObjectToDictionary(routeValues);
            if (!dict.ContainsKey(Consts.Consts.RouteExternalService) && !dict.ContainsKey(Consts.Consts.RouteServiceKey) && RequestContext.RouteData.DataTokens.ContainsKey(Consts.Consts.RouteServiceKey))
                dict[Consts.Consts.RouteServiceKey] = RequestContext.RouteData.DataTokens[Consts.Consts.RouteServiceKey];
            dict.Remove(Consts.Consts.RouteExternalService);
            return base.Action(actionName, dict);
        }

        public override string Action(string actionName, RouteValueDictionary routeValues)
        {
            if (!routeValues.ContainsKey(Consts.Consts.RouteExternalService) && !routeValues.ContainsKey(Consts.Consts.RouteServiceKey) && RequestContext.RouteData.DataTokens.ContainsKey(Consts.Consts.RouteServiceKey))
                routeValues[Consts.Consts.RouteServiceKey] = RequestContext.RouteData.DataTokens[Consts.Consts.RouteServiceKey];
            routeValues.Remove(Consts.Consts.RouteExternalService);
            return base.Action(actionName, routeValues);
        }

        public override string Action(string actionName, string controllerName)
        {
            var routeValues = new RouteValueDictionary();
            if (RequestContext.RouteData.DataTokens.ContainsKey(Consts.Consts.RouteServiceKey))
                routeValues[Consts.Consts.RouteServiceKey] = RequestContext.RouteData.DataTokens[Consts.Consts.RouteServiceKey];
            return base.Action(actionName, routeValues);
        }

        public override string Action(string actionName, string controllerName, object routeValues)
        {
            var dict = TypeHelper.ObjectToDictionary(routeValues);
            if (!dict.ContainsKey(Consts.Consts.RouteExternalService) && !dict.ContainsKey(Consts.Consts.RouteServiceKey) && RequestContext.RouteData.DataTokens.ContainsKey(Consts.Consts.RouteServiceKey))
                dict[Consts.Consts.RouteServiceKey] = RequestContext.RouteData.DataTokens[Consts.Consts.RouteServiceKey];
            dict.Remove(Consts.Consts.RouteExternalService);
            return base.Action(actionName, controllerName, dict);
        }

        public override string Action(string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            if (!routeValues.ContainsKey(Consts.Consts.RouteExternalService) && !routeValues.ContainsKey(Consts.Consts.RouteServiceKey) && RequestContext.RouteData.DataTokens.ContainsKey(Consts.Consts.RouteServiceKey))
                routeValues[Consts.Consts.RouteServiceKey] = RequestContext.RouteData.DataTokens[Consts.Consts.RouteServiceKey];
            routeValues.Remove(Consts.Consts.RouteExternalService);
            return base.Action(actionName, controllerName, routeValues);
        }

        public override string Action(string actionName, string controllerName, RouteValueDictionary routeValues, string protocol)
        {
            if (!routeValues.ContainsKey(Consts.Consts.RouteExternalService) && !routeValues.ContainsKey(Consts.Consts.RouteServiceKey) && RequestContext.RouteData.DataTokens.ContainsKey(Consts.Consts.RouteServiceKey))
                routeValues[Consts.Consts.RouteServiceKey] = RequestContext.RouteData.DataTokens[Consts.Consts.RouteServiceKey];
            routeValues.Remove(Consts.Consts.RouteExternalService);
            return base.Action(actionName, controllerName, routeValues, protocol);
        }

        public override string Action(string actionName, string controllerName, object routeValues, string protocol)
        {
            var dict = TypeHelper.ObjectToDictionary(routeValues);
            if (!dict.ContainsKey(Consts.Consts.RouteExternalService) && !dict.ContainsKey(Consts.Consts.RouteServiceKey) && RequestContext.RouteData.DataTokens.ContainsKey(Consts.Consts.RouteServiceKey))
                dict[Consts.Consts.RouteServiceKey] = RequestContext.RouteData.DataTokens[Consts.Consts.RouteServiceKey];
            dict.Remove(Consts.Consts.RouteExternalService);
            return base.Action(actionName, controllerName, dict, protocol);
        }

        public override string Action(string actionName, string controllerName, RouteValueDictionary routeValues, string protocol, string hostName)
        {
            if (!routeValues.ContainsKey(Consts.Consts.RouteExternalService) && !routeValues.ContainsKey(Consts.Consts.RouteServiceKey) && RequestContext.RouteData.DataTokens.ContainsKey(Consts.Consts.RouteServiceKey))
                routeValues[Consts.Consts.RouteServiceKey] = RequestContext.RouteData.DataTokens[Consts.Consts.RouteServiceKey];
            routeValues.Remove(Consts.Consts.RouteExternalService);
            return base.Action(actionName, controllerName, routeValues, protocol, hostName);
        }
    }
}
