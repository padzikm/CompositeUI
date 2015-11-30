using System.Web.Mvc;
using HomeManager.Service.Infrastructure.Consts;
using HomeManager.Service.Infrastructure.Routing;

namespace HomeManager.Customers.Web.Areas.TestArea
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
                CustomersConsts.ServiceName + "_TestArea_default",
                CustomersConsts.ServiceName + "/TestArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }

        protected override string RouteServiceValue
        {
            get { return CustomersConsts.RouteServiceValue; }
        }
    }
}