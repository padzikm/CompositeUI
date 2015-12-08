using CompositeUI.Infrastructure;
using CompositeUI.Orders.Web.Windsor;

namespace CompositeUI.Orders.Web.Infrastructure
{
    public class OrdersAppConfiguration : IApplicationConfiguration
    {
        public void ApplicationStart()
        {
        }

        public void ApplicationEnd()
        {
            WindsorBootstraper.Container.Dispose();
        }
    }
}