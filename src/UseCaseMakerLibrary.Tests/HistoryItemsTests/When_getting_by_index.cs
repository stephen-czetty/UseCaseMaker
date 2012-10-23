using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.HistoryItemsTests
{
    [Subject(typeof(HistoryItems))]
    public class When_getting_by_index : HistoryItemsTestBase
    {
        private It Should_return_correct_document = () => HistoryItems[0].ShouldEqual(HistoryItem);
    }
}