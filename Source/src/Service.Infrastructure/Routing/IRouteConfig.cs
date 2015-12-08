using System.Web.Routing;

namespace CompositeUI.Service.Infrastructure.Routing
{
    public interface IRouteConfig
    {
        void RegisterRoutes(RouteCollection routes);
    }
}
