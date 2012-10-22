using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.RelatedDocumentsTests
{
    [Subject(typeof(RelatedDocuments))]
    public class When_getting_by_index : RelatedDocumentsTestBase
    {
        private It Should_return_correct_document = () => RelatedDocuments[0].ShouldEqual(RelatedDocument);
    }
}