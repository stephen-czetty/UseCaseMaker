using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ActorsTests
{
    [Subject(typeof(Actors))]
    public class When_getting_next_element_id : ActorsTestBase
    {
        private Establish Context = () => Actors.Add(new Actor{ Id = 1});

        private It Should_return_two = () => Actors.GetNextFreeId().ShouldEqual(2); 
    }
}