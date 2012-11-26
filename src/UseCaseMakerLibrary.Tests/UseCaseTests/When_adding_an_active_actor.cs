using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.UseCaseTests
{
    [Subject(typeof(UseCase))]
    public class When_adding_an_active_actor : UseCaseTestBase
    {
        private Establish Context = () =>  _actor = new Actor();

        private Because Of = () => UseCase.AddActiveActor(_actor);

        private It Should_contain_active_actor_representing_actor =
            () => ((ActiveActor)UseCase.ActiveActors[0]).ActorUniqueID.ShouldEqual(_actor.UniqueId);

        private It Should_set_is_primary_to_false =
            () => ((ActiveActor)UseCase.ActiveActors[0]).IsPrimary.ShouldBeFalse();

        private static Actor _actor;
    }
}