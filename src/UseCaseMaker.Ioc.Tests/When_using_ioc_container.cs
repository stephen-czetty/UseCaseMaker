using System;
using Machine.Specifications;
using UseCaseMakerLibrary;
using UseCaseMakerLibrary.Contracts;
using UseCaseMakerLibrary.Services;

namespace UseCaseMaker.Ioc.Tests
{
    [Subject(typeof(IocContainer))]
    public class When_using_ioc_container
    {
        private Because Of = () => _exception = Catch.Exception(() => _container = IocContainer.Instance);

        private It Should_not_throw_an_exception = () => _exception.ShouldBeNull();

        private It Should_correctly_initialize_container = () => _container.ShouldBeOfType<IocContainer>();

        private It Should_fetch_concrete_type_from_container = () => _container.Resolve<IXmlSerializerSelector>().ShouldBeOfType<XmlSerializerSelector>();

        private It Should_fetch_named_concrete_type_from_container = () => _container.Resolve<ISerializer<Model>>("Version1.0").ShouldBeOfType<LegacyXmlSerializer>();

        private It Should_return_null_when_requesting_type_with_unknown_name = () => _container.Resolve<ISerializer<Model>>("notAVerson").ShouldBeNull();

        private static Exception _exception;
        private static IocContainer _container;
    }
}