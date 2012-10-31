using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.RelatedDocumentsTests
{
    [Subject(typeof(RelatedDocuments))]
    public class When_removing_item_at_index : RelatedDocumentsTestBase
    {
        private Because Of = () =>
            {
                RelatedDocuments.Add(new RelatedDocument());
                RelatedDocuments.RemoveAt(0);
            };

        private It Should_not_contain_item = () => RelatedDocuments.ShouldNotContain(RelatedDocument);
    }
}