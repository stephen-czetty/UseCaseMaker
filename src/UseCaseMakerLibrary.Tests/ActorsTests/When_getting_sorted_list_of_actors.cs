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
            () => Actors[0].Name.ShouldEqual("B");

        private It Should_have_actor_a_in_first_position_in_sorted_state =
            () => Actors.Sorted("Name")[0].Name.ShouldEqual("A");

        private It Should_correctly_sort_when_sorting_by_id_in_all_caps =
            () => Actors.Sorted("ID")[0].Name.ShouldEqual("B");


    }
}