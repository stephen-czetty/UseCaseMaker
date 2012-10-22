using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.RelatedDocumentsTests
{
    [Subject(typeof(RelatedDocuments))]
    [Ignore("Fails with a StackOverflowException due to bug in existing code")]
    public class When_adding_range_of_related_documents : RelatedDocumentsTestBase
    {
        private Because Of = () =>
            {
                var rd = new RelatedDocuments { new RelatedDocument() };
                RelatedDocuments.AddRange(rd);
            };

        private It Should_add_items_to_collection = () => RelatedDocuments.Count.ShouldEqual(2);
    }
}