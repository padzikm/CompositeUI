using System.Reflection;
using System.Web.Mvc;

namespace HomeManager.Service.Infrastructure.Attributes
{
    public class ServiceActionAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return !controllerContext.RouteData.DataTokens.ContainsKey(Consts.Consts.RouteInternalServiceKey);
        }
    }
}
