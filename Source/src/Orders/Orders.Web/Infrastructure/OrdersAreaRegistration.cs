using System.Web.Mvc;
using HomeManager.Service.Infrastructure.Consts;
using HomeManager.Service.Infrastructure.Routing;

namespace HomeManager.Orders.Web.Infrastructure
{
    public class OrdersAreaRegistration : ServiceAreaRegistration
    {
        public override string AreaName
        {
            get { return OrdersConsts.ServiceName; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            CreateRoute(
                context,
                OrdersConsts.ServiceName + "_service",
                OrdersConsts.ServiceName + "/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "HomeManager.Orders.Web.Controllers" }
                );
        }

        protected override string RouteServiceValue
        {
            get { return OrdersConsts.RouteServiceValue; }
        }
    }
}