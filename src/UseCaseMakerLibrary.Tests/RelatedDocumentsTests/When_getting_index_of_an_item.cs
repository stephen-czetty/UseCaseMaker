using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.RelatedDocumentsTests
{
    [Subject(typeof(RelatedDocuments))]
    public class When_getting_index_of_an_item : RelatedDocumentsTestBase
    {
        private It Should_return_the_correct_index = () => RelatedDocuments.IndexOf(RelatedDocument).ShouldEqual(0);
    }
}