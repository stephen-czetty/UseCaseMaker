using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.HistoryItemsTests
{
    [Subject(typeof(HistoryItems))]
    public class When_inserting_new_item_at_index_zero : HistoryItemsTestBase
    {
        private Because Of = () =>
            {
                _newItem = new HistoryItem();
                HistoryItems.Insert(0, _newItem);
            };

        private It Should_have_new_item_at_index_zero = () => HistoryItems[0].ShouldEqual(_newItem);

        private It Should_have_old_item_at_index_one = () => HistoryItems[1].ShouldEqual(HistoryItem);

        private static HistoryItem _newItem;
    }
}