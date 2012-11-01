using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ActiveActorsTests
{
    [Subject(typeof(ActiveActors))]
    public class When_getting_active_actor_by_index : ActiveActorsTestBase
    {
        private It Should_return_the_actor = () => ActiveActors[0].ShouldEqual(ActiveActor);

        private It Should_return_the_correct_type = () => ActiveActors[0].ShouldBeOfType<ActiveActor>();

        private static Actor _actor;
    }
}