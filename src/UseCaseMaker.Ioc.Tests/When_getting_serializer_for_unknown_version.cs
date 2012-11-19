using Machine.Specifications;
using UseCaseMakerLibrary;
using UseCaseMakerLibrary.Contracts;
using UseCaseMakerLibrary.Services;

namespace UseCaseMaker.Ioc.Tests
{
    [Subject(typeof(XmlSerializerSelector))]
    public class When_getting_serializer_for_unknown_version : SerializerSelectorTestsBase
    {
        private Establish Context = () =>
            {
                _selector = new XmlSerializerSelector();
                CreateTempFile("NotAVersion");
            };

        private Because Of = () => _serializer = _selector.GetSerializerForInputFile(TempFileName);

        private It Should_return_dot_net_serializer = () => _serializer.ShouldBeOfType<DotNetXmlSerializer>();

        private static XmlSerializerSelector _selector;
        private static ISerializer<Model> _serializer;
    }
}