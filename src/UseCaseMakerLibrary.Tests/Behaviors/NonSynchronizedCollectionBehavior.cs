using System;
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

        private It Should_throw_argument_null_exception_when_calling_copy_to_with_null_array = () => Catch.Exception(() => Collection.CopyTo(null, 0)).ShouldBeOfType<ArgumentNullException>();

        private It Should_throw_argument_out_of_range_exception_when_calling_copy_to_with_index_less_than_zero = () => Catch.Exception(() => Collection.CopyTo(new object[0], -1)).ShouldBeOfType<ArgumentOutOfRangeException>();

        private It Should_throw_throw_argument_exception_when_calling_copy_to_if_array_is_smaller_than_collection = () => Catch.Exception(() => Collection.CopyTo(new object[0], 0)).ShouldBeOfType<ArgumentException>();
    }
}