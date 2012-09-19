using Autofac;
using UseCaseMakerLibrary;
using UseCaseMakerLibrary.Contracts;
using UseCaseMakerLibrary.Services;

namespace UseCaseMaker.Ioc
{
    public sealed class IocContainer
    {
        private IContainer _container;

        private IocContainer()
        {
            ConfigureContainer();
        }

        static IocContainer()
        {
            Instance = new IocContainer();
        }

        public static IocContainer Instance { get; private set; }

        private void ConfigureContainer()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<SerializedSaveDataService>().As<ISavedDataService>();
            containerBuilder.RegisterType<LegacyXmlSerializer>().As<ISerializer<Model>>();

            _container = containerBuilder.Build();
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}