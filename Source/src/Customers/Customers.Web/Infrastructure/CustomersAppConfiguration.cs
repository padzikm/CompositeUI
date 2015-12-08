using CompositeUI.Customers.Web.Windsor;
using CompositeUI.Infrastructure;

namespace CompositeUI.Customers.Web.Infrastructure
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