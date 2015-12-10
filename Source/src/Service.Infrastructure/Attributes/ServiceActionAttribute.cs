using System.Reflection;
using System.Web.Mvc;

namespace CompositeUI.Service.Infrastructure
{
    public class ServiceActionAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return !controllerContext.RouteData.DataTokens.ContainsKey(Consts.RouteInternalServiceKey);
        }
    }
}
