using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ActorsTests
{
    [Subject(typeof(Actors))]
    public class When_using_actors_as_a_generic_collection : ActorsTestBase
    {
        private It Should_return_false_when_calling_is_read_only = () => Actors.IsReadOnly.ShouldBeFalse();

        private It Should_return_true_when_calling_contains_if_collection_contains_item = () => Actors.Contains(Actor).ShouldBeTrue();

        private It Should_return_false_when_calling_contains_if_collection_does_not_contain_item =
            () => Actors.Contains(new Actor()).ShouldBeFalse();
    }
}