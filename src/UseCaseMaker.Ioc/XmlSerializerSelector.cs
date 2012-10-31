using System.IO;
using System.Xml;
using UseCaseMakerLibrary;
using UseCaseMakerLibrary.Contracts;

namespace UseCaseMaker.Ioc
{
    public class XmlSerializerSelector : IXmlSerializerSelector
    {
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