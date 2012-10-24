using System.Xml;
using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.XmlSerializeTests
{
    [Subject(typeof(XmlSerializer))]
    public class When_serializing_model : XmlSerializeTestBase
    {
        private Because Of = () => _document = XmlSerializer.XmlSerialize("mainNode", "", "1.0", Model, true);

        private It Should_not_return_null = () => _document.ShouldNotBeNull();

        private static XmlDocument _document;
    }
}