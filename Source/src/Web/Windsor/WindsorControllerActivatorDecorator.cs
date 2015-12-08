using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel;

namespace CompositeUI.Web.Windsor
{
    public class WindsorControllerActivatorDecorator : IControllerActivator
    {
        private readonly IKernel _kernel;
        private readonly IEnumerable<IControllerActivator> _activators;

        public WindsorControllerActivatorDecorator(IKernel kernel, IEnumerable<IControllerActivator> activators)
        {
            _kernel = kernel;
            _activators = activators;
        }

        public IController Create(RequestContext requestContext, Type controllerType)
        {
            if (_kernel.HasComponent(controllerType))
                return _kernel.Resolve(controllerType) as IController;

            foreach (var activator in _activators)
            {
                var controller = activator.Create(requestContext, controllerType);
                if (controller != null)
                    return controller;
            }
            return null;
        }
    }
}