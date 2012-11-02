using System.IO;
using System.Xml;
using UseCaseMakerLibrary;
using UseCaseMakerLibrary.Contracts;

namespace UseCaseMaker.Ioc
{
    /// <summary>
    /// Selects a serializer based on the version of a file.
    /// </summary>
    public class XmlSerializerSelector : IXmlSerializerSelector
    {
        /// <summary>
        /// Gets the serializer for input file.
        /// </summary>
        /// <param name="inputFile">The input filename.</param>
        /// <returns>An appropriate serializer for the version of the file.</returns>
        public ISerializer<Model> GetSerializerForInputFile(string inputFile)
        {
            string version = "";
            using (Stream inputStream = File.OpenRead(inputFile))
            using (XmlReader xmlReader = XmlReader.Create(inputStream))
            {
                while (xmlReader.Read())
                {
                    // Get the Version attribute from the first Element
                    if (xmlReader.NodeType == XmlNodeType.Element)
                    {
                        version = xmlReader.GetAttribute("Version");
                        break;
                    }
                }
            }

            return IocContainer.Instance.Resolve<ISerializer<Model>>("Version" + version) ??
                   IocContainer.Instance.Resolve<ISerializer<Model>>();
        } 
    }
}