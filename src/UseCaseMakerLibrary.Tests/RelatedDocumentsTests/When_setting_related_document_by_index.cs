using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.RelatedDocumentsTests
{
    [Subject(typeof(RelatedDocuments))]
    public class When_setting_related_document_by_index : RelatedDocumentsTestBase
    {
        private Establish Context = () => { _newRelatedDocument = new RelatedDocument(); };

        private Because Of = () => RelatedDocuments[0] = _newRelatedDocument;

        private It Should_not_change_the_value = () => RelatedDocuments.ShouldNotContain(_newRelatedDocument);

        private static RelatedDocument _newRelatedDocument;
    }
}