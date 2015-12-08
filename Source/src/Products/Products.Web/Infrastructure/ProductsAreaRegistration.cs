using System.Web.Mvc;
using CompositeUI.Service.Infrastructure.Consts;
using CompositeUI.Service.Infrastructure.Routing;

namespace CompositeUI.Products.Web.Infrastructure
{
    public class ProductsAreaRegistration : ServiceAreaRegistration
    {
        public override string AreaName
        {
            get { return ProductsConsts.ServiceName; }
        }

        protected override string RouteServiceValue
        {
            get { return ProductsConsts.RouteServiceValue; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            CreateRoute(
                context,
                ProductsConsts.ServiceName + "_service",
                ProductsConsts.ServiceName + "/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional},
                new[] { "CompositeUI.Products.Web.Controllers" }
                );
        }
    }
}