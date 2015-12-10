using System.Web.Routing;

namespace CompositeUI.Service.Infrastructure
{
    public interface IRouteConfig
    {
        void RegisterRoutes(RouteCollection routes);
    }
}
