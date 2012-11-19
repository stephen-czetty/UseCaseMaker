using System.IO;

namespace UseCaseMaker.Ioc.Tests.SerializerSelectorTests
{
    public abstract class SerializerSelectorTestsBase
    {
        protected static string TempFileName;

        protected static void CreateTempFile(string version)
        {
            TempFileName = Path.GetTempFileName();
            using (var textWriter = new StreamWriter(TempFileName))
            {
                textWriter.WriteLine(@"<?xml version=""1.0"" encoding=""utf-8"" ?>");
                textWriter.WriteLine(@"<rootNode Version=""" + version + @"""/>");
            }
        }
    }
}