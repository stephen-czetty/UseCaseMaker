using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ActorsTests
{
    [Subject(typeof(Actors))]
    public class When_getting_element_id : ActorsTestBase
    {
        private It Should_return_element_id_of_parent = () => Actors.ElementID.ShouldEqual(Package.ElementID);
    }
}