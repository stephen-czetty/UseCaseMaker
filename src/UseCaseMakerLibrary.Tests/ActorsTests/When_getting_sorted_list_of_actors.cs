using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ActorsTests
{
    [Subject(typeof(Actors))]
    public class When_getting_sorted_list_of_actors : ActorsTestBase
    {
        private Because Of = () =>
            {
                Actors.Clear();
                Actors.Add(new Actor("B", "", 1));
                Actors.Add(new Actor("A", "", 2));
            };

        private It Should_have_actor_b_in_first_position_in_unsorted_state =
            () => ((Actor)Actors[0]).Name.ShouldEqual("B");

        private It Should_have_actor_a_in_first_position_in_sorted_state =
            () => ((Actor)((Actors)Actors.Sorted("Name"))[0]).Name.ShouldEqual("A");
    }
}