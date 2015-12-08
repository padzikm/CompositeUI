using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using CompositeUI.Infrastructure;

namespace CompositeUI.Web.Windsor
{
    public class WindsorBootstraper
    {
        public static IWindsorContainer Configure(string path)
        {
            var serviceInstallers = GetServiceInstallers(path);
            var installers = new List<IWindsorInstaller>();
            installers.Add(new WindsorInstaller(path));
            installers.Add(FromAssembly.InDirectory(new AssemblyFilter(path).FilterByName(p => p.Name.StartsWith("CompositeUI") && !p.Name.StartsWith("CompositeUI.Web"))));
            installers.AddRange(serviceInstallers);

            var container = new WindsorContainer();
            ConfigureUtils(container);
            container.Install(installers.ToArray());

            return container;
        }

        private static void ConfigureUtils(IWindsorContainer container)
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));
            container.AddFacility<TypedFactoryFacility>();
        }

        private static IEnumerable<ServiceWindsorInstaller> GetServiceInstallers(string binPath)
        {
            var installers = new List<ServiceWindsorInstaller>();
            var asssemblies = BuildManager.GetReferencedAssemblies();
            foreach (var elem in asssemblies)
            {
                var assembly = elem as Assembly;
                if (assembly == null)
                    continue;
                var publicTypes = assembly.GetExportedTypes();
                var serviceWindsorInstallerType = publicTypes.SingleOrDefault(p => typeof(ServiceWindsorInstaller).IsAssignableFrom(p) && !p.IsAbstract);
                if (serviceWindsorInstallerType == null)
                    continue;
                var obj = Activator.CreateInstance(serviceWindsorInstallerType, binPath);
                var serviceWindsorInstallerInstance = obj as ServiceWindsorInstaller;
                installers.Add(serviceWindsorInstallerInstance);
            }
            return installers;
        } 
    }
}