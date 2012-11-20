using Autofac;
using UseCaseMakerLibrary.Annotations;
using UseCaseMakerLibrary.Contracts;
using UseCaseMakerLibrary.Services;

namespace UseCaseMaker.Ioc.Modules
{
    /// <summary>
    /// Ioc Registration for Services
    /// </summary>
    [UsedImplicitly]
    internal sealed class ServicesModule : Module
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LocalizationService>().As<ILocalizationService>().SingleInstance();
        }
    }
}