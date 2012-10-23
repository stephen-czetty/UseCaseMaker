using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.HistoryItemsTests
{
    [Subject(typeof(HistoryItems))]
    public class When_removing_item : HistoryItemsTestBase
    {
        private Because Of = () => HistoryItems.Remove(HistoryItem);

        private It Should_not_contain_item = () => HistoryItems.ShouldNotContain(HistoryItem);
    }
}