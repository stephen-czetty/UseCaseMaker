using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ActorsTests
{
    [Subject(typeof(Actors))]
    public class When_getting_actor_by_index : ActorsTestBase
    {
        private It Should_return_the_actor = () => Actors[0].ShouldEqual(Actor);

        private It Should_return_the_correct_type = () => Actors[0].ShouldBeOfType<Actor>();
        
        private static Actor _actor;
    }
}