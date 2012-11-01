using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ActiveActorsTests
{
    [Subject(typeof(ActiveActors))]
    public class When_checking_if_collection_contains_item : ActiveActorsTestBase
    {
        private It Should_return_true_if_item_exists = () => ActiveActors.Contains(ActiveActor).ShouldBeTrue();

        private It Should_return_false_if_item_does_not_exist =
            () => ActiveActors.Contains(new ActiveActor()).ShouldBeFalse();
    }
}