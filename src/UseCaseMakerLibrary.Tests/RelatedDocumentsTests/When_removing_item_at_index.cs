using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.RelatedDocumentsTests
{
    [Subject(typeof(RelatedDocuments))]
    public class When_removing_item_at_index : RelatedDocumentsTestBase
    {
        private Because Of = () =>
            {
                RelatedDocuments.Insert(0, new RelatedDocument());
                RelatedDocuments.RemoveAt(1);
            };

        private It Should_not_contain_item = () => RelatedDocuments.ShouldNotContain(RelatedDocument);
    }
}