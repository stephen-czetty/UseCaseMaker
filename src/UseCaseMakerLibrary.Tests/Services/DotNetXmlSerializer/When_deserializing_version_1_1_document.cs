using System.IO;
using Machine.Specifications;
using UseCaseMakerLibrary.Contracts;

namespace UseCaseMakerLibrary.Tests.Services.DotNetXmlSerializer
{
    [Subject(typeof(UseCaseMakerLibrary.Services.DotNetXmlSerializer))]
    public class When_deserializing_version_1_1_document
    {
        private const string SavedDocument =
            @"<?xml version=""1.0"" encoding=""utf-8""?>
<UCM-Document xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" Version=""1.1"">
  <Model UniqueId=""32e43cb0-8050-4597-8a61-dcba1496888d"" Name=""Test"" Id=""1"" Prefix=""M"">
  </Model>
</UCM-Document>";

        private Establish Context = () =>
            {
                _textReader = new StringReader(SavedDocument);
                _serializer = new UseCaseMakerLibrary.Services.DotNetXmlSerializer();
            };

        private Because Of = () => _model = _serializer.DeSerialize(_textReader);

        private It Should_return_model = () => _model.ShouldNotBeNull();

        private It Should_set_model_unique_id_correctly = () => _model.UniqueId.ShouldEqual("32e43cb0-8050-4597-8a61-dcba1496888d");

        private It Should_set_model_id_correctly = () => _model.Id.ShouldEqual(1);

        private static TextReader _textReader;
        private static ISerializer<IModel> _serializer;
        private static IModel _model;
    }
}