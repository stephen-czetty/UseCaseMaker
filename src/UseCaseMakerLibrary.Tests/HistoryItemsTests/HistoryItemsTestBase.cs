using Machine.Specifications;

using UseCaseMakerLibrary.Tests.Behaviors;

namespace UseCaseMakerLibrary.Tests.HistoryItemsTests
{
    [Subject("HistoryItems Tests")]
    public abstract class HistoryItemsTestBase : CollectionTestBase
    {
        private Establish Context = () =>
            {
                HistoryItem = new HistoryItem();
                HistoryItems = new HistoryItems { HistoryItem };
                ExpectedCount = 1;
            };

        protected static HistoryItem HistoryItem;

        protected static HistoryItems HistoryItems
        {
            get
            {
                return (HistoryItems)Collection;
            }

            set
            {
                Collection = value;
            }
        }
    }
}