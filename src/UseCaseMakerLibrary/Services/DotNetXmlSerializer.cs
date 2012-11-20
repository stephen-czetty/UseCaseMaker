using System.IO;
using System.Xml.Serialization;
using UseCaseMakerLibrary.Contracts;

namespace UseCaseMakerLibrary.Services
{
    public class DotNetXmlSerializer : ISerializer<IModel>
    {
        public IModel DeSerialize(TextReader inputDataStream)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(UcmDocument));
            UcmDocument obj;
            try
            {
                obj = serializer.Deserialize(inputDataStream) as UcmDocument;
            }
            catch
            {
                throw new XmlSerializerException("Could not decode XML file");
            }
            return obj.Model;
        }

        public void Serialize(IModel data, TextWriter outputDataStream)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof (UcmDocument));
            serializer.Serialize(outputDataStream, new UcmDocument { Model = (Model)data });
        }

        [XmlRoot("UCM-Document")]
        public class UcmDocument
        {
            [XmlAttribute]
            public string Version
            {
                get
                {
                    return "1.1";
                }

                set
                {
                    if (value != "1.1")
                        throw new XmlSerializerException("Invalid version for this type");
                }
            }

            public Model Model { get; set; }
        }
    }
}