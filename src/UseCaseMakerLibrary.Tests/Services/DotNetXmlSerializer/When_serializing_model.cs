using System;
using System.IO;
using System.Xml;
using Machine.Specifications;
using UseCaseMakerLibrary.Contracts;

namespace UseCaseMakerLibrary.Tests.Services.DotNetXmlSerializer
{
    [Subject(typeof(UseCaseMakerLibrary.Services.DotNetXmlSerializer))]
    public class When_serializing_model
    {
        private Establish Context = () =>
            {
                _model = new Model();
                _serializer = new UseCaseMakerLibrary.Services.DotNetXmlSerializer();
                _memoryStream = new MemoryStream();
                _textWriter = new StreamWriter(_memoryStream);
            };

        private Because Of = () =>
            {
                _serializer.Serialize(_model, _textWriter);
                _memoryStream.Seek(0, SeekOrigin.Begin);
                _xmlDocument = new XmlDocument();
                _exception = Catch.Exception(() => _xmlDocument.Load(_memoryStream));
            };

        private It Should_serialize_to_valid_xml = () => _exception.ShouldBeNull();

        // ReSharper disable PossibleNullReferenceException
        private It Should_serialize_model_element_with_correct_unique_id =
            () => _xmlDocument.GetElementsByTagName("Model")[0].Attributes["UniqueId"].Value.ShouldEqual(_model.UniqueId);

        private It Should_serialize_document_with_correct_version = () => _xmlDocument.GetElementsByTagName("UCM-Document")[0].Attributes["Version"].Value.ShouldEqual("1.1");
        // ReSharper restore PossibleNullReferenceException

        private static IModel _model;
        private static ISerializer<IModel> _serializer;
        private static MemoryStream _memoryStream;
        private static TextWriter _textWriter;
        private static XmlDocument _xmlDocument;
        private static Exception _exception;
    }
}