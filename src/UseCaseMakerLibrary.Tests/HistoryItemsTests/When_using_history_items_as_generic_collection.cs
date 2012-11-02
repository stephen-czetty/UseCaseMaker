using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.HistoryItemsTests
{
    [Subject(typeof(HistoryItems))]
    public class When_using_history_items_as_generic_collection : HistoryItemsTestBase
    {
        private It Should_return_false_when_calling_is_read_only = () => HistoryItems.IsReadOnly.ShouldBeFalse();
    }
}