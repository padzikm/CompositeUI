using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using CompositeUI.Infrastructure;
using CompositeUI.Web.Windsor;

namespace CompositeUI.Web
{
    public class MvcApplication : HttpApplication
    {
        private static volatile IWindsorContainer _container;
        private static readonly object _lockObject = new object();
        
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ConfigureContainer();
            DependencyResolver.SetResolver(_container.Resolve<IDependencyResolver>());
            //ConfigureViewEngines();
            ConfigureStartups();
        }

        private void ConfigureContainer()
        {
            if (_container == null)
                lock (_lockObject)
                    if (_container == null)
                    {
                        var binPath = Server.MapPath("bin");
                        _container = WindsorBootstraper.Configure(binPath);
                    }
        }

        private void ConfigureViewEngines()
        {
            ViewEngines.Engines.Clear();
            var viewEngiens = _container.ResolveAll<IViewEngine>();
            if (viewEngiens != null)
                foreach (var viewEngine in viewEngiens)
                    ViewEngines.Engines.Add(viewEngine);
            ViewEngines.Engines.Add(new RazorViewEngine());
        }

        private void ConfigureStartups()
        {
            var configurations = _container.ResolveAll<IApplicationConfiguration>();
            foreach (var configuration in configurations)
                configuration.ApplicationStart();
        }

        protected void Application_End()
        {
            var configurations = _container.ResolveAll<IApplicationConfiguration>();
            foreach (var configuration in configurations)
                configuration.ApplicationEnd();
            _container.Dispose();
        }
    }
}
