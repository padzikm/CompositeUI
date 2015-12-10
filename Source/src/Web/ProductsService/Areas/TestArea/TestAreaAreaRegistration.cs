using System.Web.Mvc;
using CompositeUI.Service.Infrastructure;

namespace CompositeUI.Products.Web.Areas.TestArea
{
    public class TestAreaAreaRegistration : ServiceAreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TestArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            CreateRoute(
                context,
                ProductsConsts.ServiceName + "_testArea_default",
                ProductsConsts.ServiceName + "/TestArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }

        protected override string RouteServiceValue
        {
            get { return ProductsConsts.RouteServiceValue; }
        }
    }
}