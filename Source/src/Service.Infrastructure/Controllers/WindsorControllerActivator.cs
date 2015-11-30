using System;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel;

namespace HomeManager.Service.Infrastructure.Controllers
{
    public class WindsorControllerActivator : IControllerActivator
    {
        private readonly IKernel _kernel;

        public WindsorControllerActivator(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IController Create(RequestContext requestContext, Type controllerType)
        {
            return _kernel.HasComponent(controllerType) ? _kernel.Resolve(controllerType) as IController : null;
        }
    }
}