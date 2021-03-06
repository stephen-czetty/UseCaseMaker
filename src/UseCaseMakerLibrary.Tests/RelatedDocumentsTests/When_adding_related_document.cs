using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.RelatedDocumentsTests
{
    [Subject(typeof(RelatedDocuments))]
    public class When_adding_related_document : RelatedDocumentsTestBase
    {
        private Establish Context = () => { _newRelatedDocument = new RelatedDocument(); };

        private Because Of = () => RelatedDocuments.Add(_newRelatedDocument);

        private It Should_contain_new_item = () => RelatedDocuments.ShouldContain(_newRelatedDocument);

        private static RelatedDocument _newRelatedDocument;
    }
}