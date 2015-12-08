using System;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel;
using IDependencyResolver = System.Web.Mvc.IDependencyResolver;

namespace CompositeUI.Web.Windsor
{
    public class WindsorDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public WindsorDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }

        public object GetService(Type serviceType)
        {
            return _kernel.HasComponent(serviceType) ? _kernel.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.HasComponent(serviceType) ? _kernel.ResolveAll(serviceType).Cast<object>() : new object[] { };
        }
    }
}