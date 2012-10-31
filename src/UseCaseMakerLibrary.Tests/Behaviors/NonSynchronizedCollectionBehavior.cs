using System.Linq;

using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.Behaviors
{
    [Behaviors]
    public class NonSynchronizedCollectionBehavior : CollectionTestBase
    {
        private It Should_return_the_expected_count = () => Collection.Count.ShouldEqual(ExpectedCount);

        private It Should_return_a_non_null_syncroot = () => Collection.SyncRoot.ShouldNotBeNull();

        private It Should_copy_all_values_to_array = () =>
            {
                var arr = new object[Collection.Count];
                Collection.CopyTo(arr, 0);
                arr.Count(x => x != null).ShouldEqual(Collection.Count);
            };

        private It Should_return_non_null_enumerator = () => Collection.GetEnumerator().ShouldNotBeNull();

        private It Should_return_correct_synchronization_status = () => Collection.IsSynchronized.ShouldBeFalse();
    }
}