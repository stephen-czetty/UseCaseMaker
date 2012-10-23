using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.HistoryItemsTests
{
    [Subject(typeof(HistoryItems))]
    public class When_getting_index_of_an_item : HistoryItemsTestBase
    {
        private It Should_return_the_correct_index = () => HistoryItems.IndexOf(HistoryItem).ShouldEqual(0);
    }
}