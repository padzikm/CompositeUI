using System.Web;
using HomeManager.Customers.Web.Infrastructure;

[assembly: PreApplicationStartMethod(typeof(CustomersModuleRegistration), "RegisterModule")]
namespace HomeManager.Customers.Web.Infrastructure
{
    public class CustomersModuleRegistration
    {
        public static void RegisterModule()
        {
            //HttpApplication.RegisterModule(typeof(NewsModule));
        }
    }
}