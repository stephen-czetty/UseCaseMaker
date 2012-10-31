using Machine.Specifications;

using UseCaseMakerLibrary.Tests.Behaviors;

namespace UseCaseMakerLibrary.Tests.HistoryItemsTests
{
    [Subject(typeof(HistoryItems))]
    public class When_using_history_items_as_a_collection : HistoryItemsTestBase
    {
        private Behaves_like<NonSynchronizedCollectionBehavior<HistoryItem>> a_non_synchronized_collection;
    }
}