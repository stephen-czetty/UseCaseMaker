using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.RelatedDocumentsTests
{
    [Subject(typeof(RelatedDocuments))]
    public class When_copying_to_related_document_array : RelatedDocumentsTestBase
    {
        private Because Of = () => RelatedDocuments.CopyTo(_relatedDocumentArray, 0);

        private It Should_copy_the_item_into_the_array = () => _relatedDocumentArray.ShouldContain(RelatedDocument);

        private static RelatedDocument[] _relatedDocumentArray = new RelatedDocument[1];
    }
}