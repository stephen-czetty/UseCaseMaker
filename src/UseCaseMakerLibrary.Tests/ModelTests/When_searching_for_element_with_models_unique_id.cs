using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ModelTests
{
    [Subject(typeof(Model))]
    public class When_searching_for_element_with_models_unique_id : ModelTestBase
    {
        private It Should_return_the_model = () => Model.FindElementByUniqueId(Model.UniqueID).ShouldEqual(Model);
    }
}