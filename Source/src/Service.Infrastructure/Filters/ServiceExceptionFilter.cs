using System.Web.Mvc;

namespace CompositeUI.Service.Infrastructure
{
    public class ServiceExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.RouteData.DataTokens.ContainsKey(Consts.RouteInternalServiceKey) && (string)filterContext.RouteData.DataTokens[Consts.RouteInternalServiceKey] == Consts.RouteInternalServiceValue)
            {
                filterContext.Result = new ViewModelResult(new ExceptionViewModel(filterContext.Exception));
                filterContext.ExceptionHandled = true;
            }
        }
    }
}
