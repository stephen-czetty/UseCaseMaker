using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.XmlSerializeTests
{
    [Subject("XmlSerialize Tests")]
    public class XmlSerializeTestBase
    {
        private Establish Context = () =>
            {
                Model = new Model();
                Model.Actors.Add(new Actor());
            };

        protected static Model Model;
    }
}