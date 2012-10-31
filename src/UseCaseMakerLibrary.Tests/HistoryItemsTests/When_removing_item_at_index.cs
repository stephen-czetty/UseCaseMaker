using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.HistoryItemsTests
{
    [Subject(typeof(HistoryItems))]
    public class When_removing_item_at_index : HistoryItemsTestBase
    {
        private Because Of = () =>
            {
                HistoryItems.Add(new HistoryItem());
                HistoryItems.RemoveAt(0);
            };

        private It Should_not_contain_item = () => HistoryItems.ShouldNotContain(HistoryItem);
    }
}