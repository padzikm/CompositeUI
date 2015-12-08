using System.Web;
using CompositeUI.Customers.Web.Infrastructure;

[assembly: PreApplicationStartMethod(typeof(CustomersModuleRegistration), "RegisterModule")]
namespace CompositeUI.Customers.Web.Infrastructure
{
    public class CustomersModuleRegistration
    {
        public static void RegisterModule()
        {
            //HttpApplication.RegisterModule(typeof(NewsModule));
        }
    }
}