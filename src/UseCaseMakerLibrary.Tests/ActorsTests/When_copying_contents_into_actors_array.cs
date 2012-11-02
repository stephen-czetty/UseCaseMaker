using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ActorsTests
{
    [Subject(typeof(Actors))]
    public class When_copying_contents_into_actors_array : ActorsTestBase
    {
        private Because Of = () => Actors.CopyTo(_actorArray, 0);

        private It Should_copy_the_actor_into_new_array = () => _actorArray.ShouldContain(Actor);

        private static Actor[] _actorArray = new Actor[1];
    }
}