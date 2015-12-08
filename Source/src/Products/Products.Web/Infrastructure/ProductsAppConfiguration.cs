using CompositeUI.Infrastructure;
using CompositeUI.Products.Web.Windsor;

namespace CompositeUI.Products.Web.Infrastructure
{
    public class ProductsAppConfiguration : IApplicationConfiguration
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