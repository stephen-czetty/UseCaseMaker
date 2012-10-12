using System.IO;
using UseCaseMakerLibrary.Contracts;

namespace UseCaseMakerLibrary.Services
{
    public class DotNetXmlSerializer : ISerializer<Model>
    {
        public Model DeSerialize(TextReader inputDataStream)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof (Model));
            var obj = serializer.Deserialize(inputDataStream) as Model;
            if (obj == null)
                throw new XmlSerializerException("Could not decode XML file");
            return obj;
        }

        public void Serialize(Model data, TextWriter outputDataStream)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof (Model));
            serializer.Serialize(outputDataStream, data);
        }
    }
}