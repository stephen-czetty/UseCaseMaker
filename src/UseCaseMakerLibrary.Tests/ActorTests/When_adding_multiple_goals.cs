using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ActorTests
{
    [Subject(typeof(Actor))]
    public class When_adding_multiple_goals : ActorTestsBase
    {
        private Because Of = () =>
            {
                _idx1 = Actor.AddGoal();
                _idx2 = Actor.AddGoal();
            };

        private It Should_return_valid_index_for_first_goal = () => _idx1.ShouldBeGreaterThanOrEqualTo(0);

        private It Should_return_valid_index_for_second_goal = () => _idx2.ShouldBeGreaterThanOrEqualTo(0);

        private It Should_add_first_goal_to_goals_list = () => Actor.Goals[_idx1].ShouldBeOfType<Goal>();

        private It Should_add_second_goal_to_goals_list = () => Actor.Goals[_idx2].ShouldBeOfType<Goal>();

        private It Should_set_first_goals_id_to_one = () => ((Goal)Actor.Goals[_idx1]).ID.ShouldEqual(1);

        private It Should_set_second_goals_id_to_two = () => ((Goal)Actor.Goals[_idx2]).ID.ShouldEqual(2);

        private static int _idx1;
        private static int _idx2;
    }
}