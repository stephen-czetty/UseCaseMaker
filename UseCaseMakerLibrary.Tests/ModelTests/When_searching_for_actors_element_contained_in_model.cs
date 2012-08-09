using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ModelTests
{
    [Subject(typeof(Model))]
    public class When_searching_for_actors_element_contained_in_model : ModelTestBase
    {
        // ISSUE: API is clunky
        private It Should_return_the_actors =
            () => Model.FindElementByUniqueID(Model.Actors.UniqueID).ShouldEqual(Model.Actors);
    }
}