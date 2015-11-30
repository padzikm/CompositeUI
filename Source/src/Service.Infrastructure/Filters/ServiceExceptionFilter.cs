using System.Web.Mvc;
using HomeManager.Service.Infrastructure.ViewModels;

namespace HomeManager.Service.Infrastructure.Filters
{
    public class ServiceExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.RouteData.DataTokens.ContainsKey(Consts.Consts.RouteInternalServiceKey) && (string)filterContext.RouteData.DataTokens[Consts.Consts.RouteInternalServiceKey] == Consts.Consts.RouteInternalServiceValue)
            {
                filterContext.Result = new ViewModelResult(new ExceptionViewModel(filterContext.Exception));
                filterContext.ExceptionHandled = true;
            }
        }
    }
}
