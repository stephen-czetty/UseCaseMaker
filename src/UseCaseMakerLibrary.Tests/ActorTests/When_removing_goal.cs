using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ActorTests
{
    [Subject(typeof(Actor))]
    public class When_removing_goal : ActorTestsBase
    {
        private Because Of = () =>
            {
                int idx = Actor.AddGoal();
                _goalToRemove = (Goal)Actor.Goals[idx];
                idx = Actor.AddGoal();
                _goalToKeep = (Goal)Actor.Goals[idx];

                _goalToKeep.ID.ShouldBeGreaterThan(_goalToRemove.ID);

                Actor.RemoveGoal(_goalToRemove);
            };

        private It Should_remove_the_goal = () => Actor.Goals.ShouldNotContain(_goalToRemove);

        private It Should_keep_the_remaining_goal = () => Actor.Goals.ShouldContain(_goalToKeep);

        private It Should_renumber_the_id_on_the_remaining_goal = () => ((Goal)Actor.Goals[0]).ID.ShouldEqual(1);

        private static Goal _goalToRemove;
        private static Goal _goalToKeep;
    }
}