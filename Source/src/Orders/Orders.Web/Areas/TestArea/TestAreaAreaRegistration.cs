using System.Web.Mvc;
using CompositeUI.Service.Infrastructure;

namespace CompositeUI.Orders.Web.Areas.TestArea
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
                OrdersConsts.ServiceName + "_TestArea_default",
                OrdersConsts.ServiceName + "/TestArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }

        protected override string RouteServiceValue
        {
            get { return OrdersConsts.RouteServiceValue; }
        }
    }
}