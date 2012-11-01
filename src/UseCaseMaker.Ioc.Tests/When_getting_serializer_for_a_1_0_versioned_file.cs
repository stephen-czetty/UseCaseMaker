using System.IO;
using Machine.Specifications;
using UseCaseMakerLibrary;
using UseCaseMakerLibrary.Contracts;
using UseCaseMakerLibrary.Services;

namespace UseCaseMaker.Ioc.Tests
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

    public abstract class SerializerSelectorTestsBase
    {
        protected static string TempFileName;

        protected static void CreateTempFile(string version)
        {
            TempFileName = Path.GetTempFileName();
            using (var textWriter = new StreamWriter(TempFileName))
            {
                textWriter.WriteLine(@"<?xml version=""1.0"" encoding=""utf-8"" ?>");
                textWriter.WriteLine(@"<rootNode Version=""" + version + @"""/>");
            }
        }
    }

    [Subject(typeof(XmlSerializerSelector))]
    public class When_getting_serializer_for_a_1_1_versioned_file : SerializerSelectorTestsBase
    {
        private Establish Context = () =>
        {
            _selector = new XmlSerializerSelector();
            CreateTempFile("1.1");
        };

        private Because Of = () => _serializer = _selector.GetSerializerForInputFile(TempFileName);

        private It Should_return_dot_net_serializer = () => _serializer.ShouldBeOfType<DotNetXmlSerializer>();

        private static XmlSerializerSelector _selector;
        private static ISerializer<Model> _serializer;
    }

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