using System.Web.Mvc;
using HomeManager.Service.Infrastructure.Consts;
using HomeManager.Service.Infrastructure.Routing;

namespace HomeManager.Customers.Web.Infrastructure
{
    public class CustomersAreaRegistration : ServiceAreaRegistration
    {
        public override string AreaName
        {
            get { return CustomersConsts.ServiceName; }
        }

        protected override string RouteServiceValue
        {
            get { return CustomersConsts.RouteServiceValue; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            CreateRoute(
                context,
                CustomersConsts.ServiceName + "_service",
                CustomersConsts.ServiceName + "/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional},
                new[] { "HomeManager.Customers.Web.Controllers" }
                );
        }
    }
}