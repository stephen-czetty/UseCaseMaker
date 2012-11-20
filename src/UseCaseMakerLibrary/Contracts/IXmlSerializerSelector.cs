using System.IO;

namespace UseCaseMakerLibrary.Contracts
{
    public interface IXmlSerializerSelector
    {
        ISerializer<IModel> GetSerializerForInputFile(string inputFile);
    }
}