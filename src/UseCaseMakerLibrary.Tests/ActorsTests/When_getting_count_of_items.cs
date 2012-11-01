using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ActorsTests
{
    [Subject(typeof(Actors))]
    public class When_getting_count_of_items : ActorsTestBase
    {
        private Because Of = () => Actors.Add(new Actor());

        private It Should_return_the_number_of_actors = () => Actors.Count.ShouldEqual(2);
    }
}