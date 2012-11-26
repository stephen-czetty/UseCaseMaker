using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ActorTests
{
    [Subject(typeof(Actor))]
    public class When_getting_goal_by_unique_id : ActorTestsBase
    {
        private Because Of = () =>
            {
                Actor.AddGoal();
                _goal = (Goal)Actor.Goals[0];
            };

        private It Should_return_the_goal = () => Actor.FindGoalByUniqueID(_goal.UniqueId).ShouldEqual(_goal);

        private static Goal _goal;
    }
}