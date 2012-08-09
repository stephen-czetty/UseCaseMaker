using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ModelTests
{
    [Subject(typeof(Model))]
    public class When_searching_for_requirements_element_contained_in_model : ModelTestBase
    {
        // ISSUE: API is clunky
        private It Should_return_the_requirement =
            () => Model.FindElementByUniqueID(Model.Requirements.UniqueID).ShouldEqual(Model.Requirements);
    }
}