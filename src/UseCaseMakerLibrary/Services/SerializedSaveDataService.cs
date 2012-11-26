using System.IO;
using UseCaseMakerLibrary.Contracts;

namespace UseCaseMakerLibrary.Services
{
    /// <summary>
    /// Service for saving data using serialization
    /// </summary>
    public class SerializedSaveDataService : ISavedDataService
    {
        /// <summary>
        /// Serializer for an <see cref="IModel"/>
        /// </summary>
        private readonly ISerializer<IModel> _serializer;

        /// <summary>
        /// Selector for the Xml Serializer needed for loading
        /// </summary>
        private readonly IXmlSerializerSelector _selector;

        /// <summary>
        /// Initializes a new instance of the <see cref="SerializedSaveDataService"/> class.
        /// </summary>
        /// <param name="serializer">The serializer.</param>
        /// <param name="selector">The selector.</param>
        public SerializedSaveDataService(ISerializer<IModel> serializer, IXmlSerializerSelector selector)
        {
            _serializer = serializer;
            _selector = selector;
        }

        /// <summary>
        /// Loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// The loaded <see cref="Model"/>
        /// </returns>
        public IModel Load(string fileName)
        {
            using (var inputStream = new StreamReader(fileName))
                return _selector.GetSerializerForInputFile(fileName).DeSerialize(inputStream);
        }

        /// <summary>
        /// Saves the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="fileName">Name of the file.</param>
        public void Save(IModel model, string fileName)
        {
            using (var outputStream = new StreamWriter(fileName, false))
                _serializer.Serialize(model, outputStream);
        }
    }
}