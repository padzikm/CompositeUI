using System.Web.Mvc;
using CompositeUI.Service.Infrastructure.Consts;
using CompositeUI.Service.Infrastructure.Routing;

namespace CompositeUI.Orders.Web.Infrastructure
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
                new[] { "CompositeUI.Orders.Web.Controllers" }
                );
        }

        protected override string RouteServiceValue
        {
            get { return OrdersConsts.RouteServiceValue; }
        }
    }
}