using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.HistoryItemsTests
{
    [Subject(typeof(HistoryItems))]
    [Ignore("Test fails due to existing bug in application")]
    public class When_adding_range_of_related_documents : HistoryItemsTestBase
    {
        private Because Of = () =>
            {
                var rd = new RelatedDocuments();
                HistoryItems.AddRange(rd);
            };

        private It Should_add_items_to_collection = () => HistoryItems.Count.ShouldEqual(1);
    }
}