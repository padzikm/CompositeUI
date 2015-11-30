using System.Web.Mvc;
using HomeManager.Service.Infrastructure.Filters;

namespace HomeManager.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ServiceExceptionFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
