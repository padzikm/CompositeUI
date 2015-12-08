using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel;

namespace CompositeUI.Web.Windsor
{
    public class WindsorControllerFactoryDecorator : DefaultControllerFactory
    {
        private readonly IKernel _kernel;
        private readonly IEnumerable<IControllerFactory> _factories;

        public WindsorControllerFactoryDecorator(IKernel kernel, IEnumerable<IControllerFactory> factories)
        {
            _kernel = kernel;
            _factories = factories;
        }


        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            var c = base.CreateController(requestContext, controllerName);
            return c;
        }

        public override void ReleaseController(IController controller)
        {
            _kernel.ReleaseComponent(controller);
            foreach (var factory in _factories)
                factory.ReleaseController(controller);
        }
    }
}