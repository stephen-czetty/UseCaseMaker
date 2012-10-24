using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ActiveActorsTests
{
    [Subject(typeof(ActiveActors))]
    public class When_getting_index_of_an_item : ActiveActorsTestBase
    {
        private It Should_return_the_correct_index = () => ActiveActors.IndexOf(ActiveActor).ShouldEqual(0);
    }
}