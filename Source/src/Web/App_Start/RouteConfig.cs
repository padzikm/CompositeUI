using System.Web.Mvc;
using System.Web.Routing;
using HomeManager.Web.Infrastructure;

namespace HomeManager.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.CreateRoute(
                name: "Default",
                namespaces: new[] { "HomeManager.Web.Controllers" },
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
