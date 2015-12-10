using System.Web.Mvc;
using CompositeUI.Service.Infrastructure;

namespace CompositeUI.Web.Areas.TestArea
{
    public class TestAreaAreaRegistration : WebAreaRegistration 
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
                "TestArea_default",
                "TestArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}