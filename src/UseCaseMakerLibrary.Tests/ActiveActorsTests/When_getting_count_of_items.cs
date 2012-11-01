using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ActiveActorsTests
{
    [Subject(typeof(ActiveActors))]
    public class When_getting_count_of_items : ActiveActorsTestBase
    {
        private Because Of = () => ActiveActors.Add(new ActiveActor());

        private It Should_return_the_number_of_actors = () => ActiveActors.Count.ShouldEqual(2);
    }
}