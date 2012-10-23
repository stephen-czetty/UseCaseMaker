using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.HistoryItemsTests
{
    [Subject(typeof(HistoryItems))]
    public class When_calling_clear : HistoryItemsTestBase
    {
        private Because Of = () => HistoryItems.Clear();

        private It Should_clear_all_items_from_the_list = () => HistoryItems.Count.ShouldEqual(0);
    }
}