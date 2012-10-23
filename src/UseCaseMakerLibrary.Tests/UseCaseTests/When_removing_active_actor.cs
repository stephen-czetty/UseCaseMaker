using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.UseCaseTests
{
    [Subject(typeof(UseCase))]
    public class When_removing_active_actor : UseCaseTestBase
    {
        private Establish Context = () =>
            {
                _actor = new Actor();
                UseCase.AddActiveActor(_actor);
                UseCase.ActiveActors.Count.ShouldEqual(1);
            };

        private Because Of = () => UseCase.RemoveActiveActor(_actor);

        private It Should_not_contain_active_actor = () => UseCase.ActiveActors.Count.ShouldEqual(0);

        private static Actor _actor;
    }
}