using System.Web.Routing;

namespace HomeManager.Service.Infrastructure.Routing
{
    public interface IRouteConfig
    {
        void RegisterRoutes(RouteCollection routes);
    }
}
