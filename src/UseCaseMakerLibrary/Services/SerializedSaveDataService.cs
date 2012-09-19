using System.IO;
using UseCaseMakerLibrary.Contracts;

namespace UseCaseMakerLibrary.Services
{
    public class SerializedSaveDataService : ISavedDataService
    {
        private readonly ISerializer<Model> _serializer;

        public SerializedSaveDataService(ISerializer<Model> serializer)
        {
            _serializer = serializer;
        }

        public Model Load(string fileName)
        {
            using (var inputStream = new StreamReader(fileName))
                return _serializer.DeSerialize(inputStream);
        }

        public void Save(Model model, string fileName)
        {
            using (var outputStream = new StreamWriter(fileName, false))
                _serializer.Serialize(model, outputStream);
        }
    }
}