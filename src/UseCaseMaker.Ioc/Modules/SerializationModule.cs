using Autofac;
using UseCaseMakerLibrary;
using UseCaseMakerLibrary.Annotations;
using UseCaseMakerLibrary.Contracts;
using UseCaseMakerLibrary.Services;

namespace UseCaseMaker.Ioc.Modules
{
    /// <summary>
    /// Ioc Registration for serialization routines
    /// </summary>
    [UsedImplicitly]
    public sealed class SerializationModule : Module
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SerializedSaveDataService>().As<ISavedDataService>();

            builder.RegisterType<XmlSerializerSelector>().As<IXmlSerializerSelector>();

            // Default save file format
            builder.RegisterType<DotNetXmlSerializer>().As<ISerializer<Model>>();

            // Save file formats
            builder.RegisterType<LegacyXmlSerializer>().Named<ISerializer<Model>>("Version1.0");
            builder.RegisterType<DotNetXmlSerializer>().Named<ISerializer<Model>>("Version1.1");
        }
    }
}