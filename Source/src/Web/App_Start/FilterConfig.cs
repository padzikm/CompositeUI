using System.Web.Mvc;
using CompositeUI.Service.Infrastructure.Filters;

namespace CompositeUI.Web
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
