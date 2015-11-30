using System.Reflection;
using System.Web.Mvc;

namespace HomeManager.Service.Infrastructure.Attributes
{
    public class InternalActionAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return controllerContext.RouteData.DataTokens.ContainsKey(Consts.Consts.RouteInternalServiceKey);
        }
    }
}
