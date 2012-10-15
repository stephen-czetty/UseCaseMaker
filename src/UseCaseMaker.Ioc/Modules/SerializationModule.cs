using Autofac;
using UseCaseMakerLibrary;
using UseCaseMakerLibrary.Contracts;
using UseCaseMakerLibrary.Services;

namespace UseCaseMaker.Ioc.Modules
{
    [UsedImplicitly]
    public sealed class SerializationModule : Module
    {
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