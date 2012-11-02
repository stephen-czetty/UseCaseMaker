using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.HistoryItemsTests
{
    [Subject(typeof(HistoryItems))]
    public class When_copying_to_history_item_array : HistoryItemsTestBase
    {
        private Because Of = () => HistoryItems.CopyTo(_historyItemArray, 0);

        private It Should_copy_the_item_into_the_array = () => _historyItemArray.ShouldContain(HistoryItem);

        private static HistoryItem[] _historyItemArray = new HistoryItem[1];
    }
}