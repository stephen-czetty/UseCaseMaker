using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.HistoryItemsTests
{
    [Subject(typeof(HistoryItems))]
    public class When_setting_history_item_by_index : HistoryItemsTestBase
    {
        private Establish Context = () => { _newHistoryItem = new HistoryItem(); };

        private Because Of = () => HistoryItems[0] = _newHistoryItem;

        private It Should_not_change_the_value = () => HistoryItems.ShouldNotContain(_newHistoryItem);

        private static HistoryItem _newHistoryItem;
    }
}