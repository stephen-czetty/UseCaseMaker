using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ActorTests
{
    [Subject(typeof(Actor))]
    public class When_adding_first_goal : ActorTestsBase
    {
        private Because Of = () => { _returnIndex = Actor.AddGoal(); };

        private It Should_return_index_zero = () => _returnIndex.ShouldEqual(0);

        private It Should_add_goal_to_goals_list = () => Actor.Goals[_returnIndex].ShouldBeOfType<Goal>();

        private It Should_set_goal_id_to_one = () => ((Goal)Actor.Goals[_returnIndex]).Id.ShouldEqual(1);

        private static int _returnIndex;
    }
}