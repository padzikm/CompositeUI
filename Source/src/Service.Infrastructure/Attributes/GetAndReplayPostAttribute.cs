using System.Reflection;
using System.Web.Mvc;

namespace CompositeUI.Service.Infrastructure
{
    public class GetAndReplayPostAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            var isGetRequest = controllerContext.HttpContext.Request.HttpMethod.ToUpper() == "GET";
            var isPostRequest = controllerContext.HttpContext.Request.HttpMethod.ToUpper() == "POST";
            var isReplayRequest = controllerContext.RouteData.Values.ContainsKey(Consts.InvalidModelStateReplayParamName);
            return isGetRequest || (isPostRequest && isReplayRequest);
        }
    }
}
