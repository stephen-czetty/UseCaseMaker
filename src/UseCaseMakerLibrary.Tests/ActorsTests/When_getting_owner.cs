using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ActorsTests
{
    [Subject(typeof(Actors))]
    public class When_getting_owner : ActorsTestBase
    {
        private It Should_return_the_correct_package = () => Actors.Owner.ShouldEqual(Package);
    }
}