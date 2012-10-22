using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ActorsTests
{
    [Subject(typeof(Actors))]
    public class When_getting_actor_by_index : ActorsTestBase
    {
        private Because Of = () =>
            {
                _actor = new Actor();
                Actors.Add(_actor);
            };

        private It Should_return_the_actor = () => Actors[0].ShouldEqual(_actor);

        private It Should_return_the_correct_type = () => Actors[0].ShouldBeOfType<Actor>();
        
        private static Actor _actor;
    }
}