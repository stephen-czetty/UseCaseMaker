using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.HistoryItemsTests
{
    [Subject(typeof(HistoryItems))]
    public class When_checking_if_collection_contains_item : HistoryItemsTestBase
    {
        private It Should_return_true_if_item_exists = () => HistoryItems.Contains(HistoryItem).ShouldBeTrue();

        private It Should_return_false_if_item_does_not_exist =
            () => HistoryItems.Contains(new RelatedDocument()).ShouldBeFalse();
    }
}