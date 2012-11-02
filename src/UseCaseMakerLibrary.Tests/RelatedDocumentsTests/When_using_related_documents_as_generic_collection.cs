using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.RelatedDocumentsTests
{
    [Subject(typeof(RelatedDocuments))]
    public class When_using_related_documents_as_generic_collection : RelatedDocumentsTestBase
    {
        private It Should_return_false_when_calling_is_read_only = () => RelatedDocuments.IsReadOnly.ShouldBeFalse();
    }
}