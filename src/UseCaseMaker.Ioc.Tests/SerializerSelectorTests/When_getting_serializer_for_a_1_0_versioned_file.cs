using Machine.Specifications;
using UseCaseMakerLibrary;
using UseCaseMakerLibrary.Contracts;
using UseCaseMakerLibrary.Services;

namespace UseCaseMaker.Ioc.Tests.SerializerSelectorTests
{
    [Subject(typeof(XmlSerializerSelector))]
    public class When_getting_serializer_for_a_1_0_versioned_file : SerializerSelectorTestsBase
    {
        private Establish Context = () =>
            {
                _selector = new XmlSerializerSelector();
                CreateTempFile("1.0");
            };

        private Because Of = () => _serializer = _selector.GetSerializerForInputFile(TempFileName);

        private It Should_return_legacy_serializer = () => _serializer.ShouldBeOfType<LegacyXmlSerializer>();

        private static XmlSerializerSelector _selector;
        private static ISerializer<Model> _serializer;
    }
}