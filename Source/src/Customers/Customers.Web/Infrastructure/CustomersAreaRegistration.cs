using System.Web.Mvc;
using CompositeUI.Service.Infrastructure.Consts;
using CompositeUI.Service.Infrastructure.Routing;

namespace CompositeUI.Customers.Web.Infrastructure
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
                new[] { "CompositeUI.Customers.Web.Controllers" }
                );
        }
    }
}