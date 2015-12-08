using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CompositeUI.Infrastructure;
using CompositeUI.Web.Common.Metadata;

namespace CompositeUI.Web.Windsor
{
    public class WindsorInstaller : IWindsorInstaller
    {
        private readonly string _path;

        public WindsorInstaller(string path)
        {
            _path = path;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            RegisterWebProjectClasses(container);
            RegisterWebCommonProjectClasses(container);
        }

        private void RegisterWebProjectClasses(IWindsorContainer container)
        {
            container.Register(Component.For<IControllerActivator>().ImplementedBy<WindsorControllerActivatorDecorator>());
            container.Register(Component.For<IDependencyResolver>().ImplementedBy<WindsorDependencyResolver>());
            container.Register(Component.For<IControllerFactory>().ImplementedBy<WindsorControllerFactoryDecorator>());
            container.Register(Classes.FromThisAssembly().BasedOn<IController>().LifestyleTransient());
        }

        private void RegisterWebCommonProjectClasses(IWindsorContainer container)
        {
            container.Register(Component.For<ModelMetadataProvider>().ImplementedBy<DataAnnotationsModelMetadataProviderDecorator>());
            container.Register(Classes.FromAssemblyContaining<DataAnnotationsModelMetadataProviderDecorator>().BasedOn(typeof(IDecorateMetadataForProperty), typeof(IDecorateMetadataForType)).WithServiceAllInterfaces());
            container.Register(Classes.FromAssemblyContaining<DataAnnotationsModelMetadataProviderDecorator>().BasedOn<ModelValidatorProvider>().WithServiceBase());
            container.Register(Classes.FromAssemblyContaining<DataAnnotationsModelMetadataProviderDecorator>().BasedOn<ValueProviderFactory>().WithServiceBase());
        }

        private void RegisterAllPublicClasses(IWindsorContainer container)
        {
            container.Register(
                Classes.FromAssemblyInDirectory(new AssemblyFilter(_path).FilterByName(p => p.Name.StartsWith("CompositeUI") && !p.Name.StartsWith("CompositeUI.Web")))
                    .Pick()
                    .Unless(p => typeof(IApplicationConfiguration).IsAssignableFrom(p))
                    .WithService.AllInterfaces()
                    .LifestylePerWebRequest());

            container.Register(
                Classes.FromAssemblyInDirectory(new AssemblyFilter(_path).FilterByName(p => p.Name.StartsWith("CompositeUI")))
                    .BasedOn<IApplicationConfiguration>()
                    .WithService.AllInterfaces());
        }
    }
}