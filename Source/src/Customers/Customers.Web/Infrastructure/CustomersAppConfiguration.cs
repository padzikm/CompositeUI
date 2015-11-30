using HomeManager.Customers.Web.Windsor;
using HomeManager.Infrastructure;

namespace HomeManager.Customers.Web.Infrastructure
{
    public class CustomersAppConfiguration : IApplicationConfiguration
    {
        public void ApplicationStart()
        {
        }

        public void ApplicationEnd()
        {
            WindsorBootstraper.NBus.Dispose();
            WindsorBootstraper.Container.Dispose();
        }
    }
}