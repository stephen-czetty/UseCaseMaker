using System.IO;

namespace UseCaseMakerLibrary.Contracts
{
    public interface IXmlSerializerSelector
    {
        ISerializer<Model> GetSerializerForInputFile(string inputFile);
    }
}