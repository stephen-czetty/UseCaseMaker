using System;
using System.IO;
using Machine.Specifications;
using UseCaseMakerLibrary.Contracts;

namespace UseCaseMakerLibrary.Tests.Services.LegacyXmlSerializer
{
    [Subject(typeof(UseCaseMakerLibrary.Services.LegacyXmlSerializer))]
    public class When_deserializing_invalid_document
    {
        private const string SavedDocument =
            @"<?xml version=""1.0"" encoding=""UTF-8""?>
<Not-A-UCM-Document Version=""1.1"">
  <Model Type=""UseCaseMakerLibrary.Model"" UniqueID=""32e43cb0-8050-4597-8a61-dcba1496888d"" Name=""Test"" ID=""1"" Prefix=""M"" Path=""M1"">
  </Model>
</Not-A-UCM-Document>";

        private Establish Context = () =>
            {
                _textReader = new StringReader(SavedDocument);
                _serializer = new UseCaseMakerLibrary.Services.LegacyXmlSerializer();
            };

        // ReSharper disable ReturnValueOfPureMethodIsNotUsed
        private Because Of = () => _exception = Catch.Exception(() => _serializer.DeSerialize(_textReader));
        // ReSharper restore ReturnValueOfPureMethodIsNotUsed

        private It Should_throw_xml_serializer_exception = () => _exception.ShouldBeOfType<XmlSerializerException>();

        private static TextReader _textReader;
        private static ISerializer<Model> _serializer;
        private static Exception _exception;
    }
}