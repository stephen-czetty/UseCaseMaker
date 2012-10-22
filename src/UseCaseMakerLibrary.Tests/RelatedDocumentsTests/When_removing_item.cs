using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.RelatedDocumentsTests
{
    [Subject(typeof(RelatedDocuments))]
    public class When_removing_item : RelatedDocumentsTestBase
    {
        private Because Of = () => RelatedDocuments.Remove(RelatedDocument);

        private It Should_not_contain_item = () => RelatedDocuments.ShouldNotContain(RelatedDocument);
    }
}