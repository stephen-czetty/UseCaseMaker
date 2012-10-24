using System.IO;
using System.Xml;
using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.XmlSerializeTests
{
    [Subject(typeof(XmlSerializer))]
    public class When_deserializing_from_xml
    {
        private static string _xmlText =
            @"<?xml version=""1.0"" encoding=""UTF-8""?><mainNode Version=""1.0""><Model Type=""UseCaseMakerLibrary.Model"" UniqueID=""8f4bd5c7-ba2a-492e-88bc-95ae7af37379"" Name="""" ID=""-1"" Prefix="""" Path=""-1""><Glossary Type=""UseCaseMakerLibrary.GlossaryItems"" UniqueID=""849743ad-c4f0-4bd0-9414-9ba7b8c3955c"" Name="""" ID=""-1"" Prefix="""" Path=""-1"" /><Actors Type=""UseCaseMakerLibrary.Actors"" UniqueID=""60fba948-497b-47ad-88a3-32f5665a806e"" Name="""" ID=""-1"" Prefix="""" Path=""-1""><Actor Type=""UseCaseMakerLibrary.Actor"" UniqueID=""85f67c70-41e3-4ebb-8d64-0c9d59fa06d8"" Name="""" ID=""-1"" Prefix="""" Path=""-1""><Attributes Type=""UseCaseMakerLibrary.CommonAttributes""><Description Type=""System.String""></Description><Notes Type=""System.String""></Notes><RelatedDocuments Type=""UseCaseMakerLibrary.RelatedDocuments"" /></Attributes><Goals Type=""UseCaseMakerLibrary.Goals"" UniqueID=""8ca49e6a-98a8-490f-a243-be43ea872f91"" Name="""" ID=""-1"" Prefix="""" Path="""" /></Actor></Actors><Packages Type=""UseCaseMakerLibrary.Packages"" UniqueID=""b05ebe03-f602-457f-b921-bbea2717c1f1"" Name="""" ID=""-1"" Prefix="""" Path=""-1"" /><UseCases Type=""UseCaseMakerLibrary.UseCases"" UniqueID=""a9050684-188c-4067-83ec-5c55ba4f00ea"" Name="""" ID=""-1"" Prefix="""" Path=""-1"" /><Requirements Type=""UseCaseMakerLibrary.Requirements"" UniqueID=""48ebbcff-842b-4033-8443-85e0db8aefeb"" Name="""" ID=""-1"" Prefix="""" Path="""" /><Attributes Type=""UseCaseMakerLibrary.CommonAttributes""><Description Type=""System.String""></Description><Notes Type=""System.String""></Notes><RelatedDocuments Type=""UseCaseMakerLibrary.RelatedDocuments"" /></Attributes></Model></mainNode>";

        private Establish Context = () =>
            {
                _document = new XmlDocument();
                using (var reader = new StringReader(_xmlText))
                    _document.Load(reader);
                _model = new Model();
            };

        private Because Of = () => XmlSerializer.XmlDeserialize(_document, "mainNode", "", "1.0", _model);

        private It Should_populate_the_model = () => _model.UniqueID.ShouldEqual("8f4bd5c7-ba2a-492e-88bc-95ae7af37379");

        private static Model _model;
        private static XmlDocument _document;
    }
}