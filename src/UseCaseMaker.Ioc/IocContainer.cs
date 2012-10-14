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

            // Xml Serialization
            containerBuilder.RegisterType<XmlSerializerSelector>().As<IXmlSerializerSelector>();
            containerBuilder.RegisterType<DotNetXmlSerializer>().As<ISerializer<Model>>();
            containerBuilder.RegisterType<LegacyXmlSerializer>().Named<ISerializer<Model>>("Version1.0");
            containerBuilder.RegisterType<DotNetXmlSerializer>().Named<ISerializer<Model>>("Version1.1");

            _container = containerBuilder.Build();
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public T Resolve<T>(string name)
        {
            object obj;
            if (_container.TryResolveNamed(name, typeof(T), out obj))
                return (T)obj;
            return default(T);
        }
    }
}