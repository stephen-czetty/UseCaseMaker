using System.IO;
using UseCaseMakerLibrary.Contracts;

namespace UseCaseMakerLibrary.Services
{
    public class SerializedSaveDataService : ISavedDataService
    {
        private readonly ISerializer<Model> _serializer;
        private readonly IXmlSerializerSelector _selector;

        public SerializedSaveDataService(ISerializer<Model> serializer, IXmlSerializerSelector selector)
        {
            _serializer = serializer;
            _selector = selector;
        }

        public Model Load(string fileName)
        {
            using (var inputStream = new StreamReader(fileName))
                return _selector.GetSerializerForInputFile(fileName).DeSerialize(inputStream);
        }

        public void Save(Model model, string fileName)
        {
            using (var outputStream = new StreamWriter(fileName, false))
                _serializer.Serialize(model, outputStream);
        }
    }
}