using System.Reflection;
using Autofac;

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

            containerBuilder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

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