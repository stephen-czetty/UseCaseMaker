using System.Reflection;
using Autofac;

namespace UseCaseMaker.Ioc
{
    /// <summary>
    /// Singleton class for resolving class instances.
    /// </summary>
    public sealed class IocContainer
    {
        /// <summary>
        /// The Autofac container
        /// </summary>
        private IContainer _container;

        /// <summary>
        /// Initializes static members of the <see cref="IocContainer"/> class.
        /// </summary>
        static IocContainer()
        {
            Instance = new IocContainer();
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="IocContainer"/> class from being created.
        /// </summary>
        private IocContainer()
        {
            ConfigureContainer();
        }

        /// <summary>
        /// Gets the Ioc instance.
        /// </summary>
        public static IocContainer Instance { get; private set; }

        /// <summary>
        /// Resolves an instance of the requested type.
        /// </summary>
        /// <typeparam name="T">The requested type.</typeparam>
        /// <returns>A concrete instance of the requested type.</returns>
        public T Resolve<T>()
        {
            return this._container.Resolve<T>();
        }

        /// <summary>
        /// Resolves a named instance of the requested type
        /// </summary>
        /// <typeparam name="T">The requested type.</typeparam>
        /// <param name="name">The name.</param>
        /// <returns>A concrete instance of the requested type.</returns>
        public T Resolve<T>(string name)
        {
            object obj;
            if (this._container.TryResolveNamed(name, typeof(T), out obj))
                return (T)obj;
            return default(T);
        }

        /// <summary>
        /// Configures the container.
        /// </summary>
        private void ConfigureContainer()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

            _container = containerBuilder.Build();
        }
    }
}