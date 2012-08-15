using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ModelTests
{
    [Subject(typeof (Model))]
    public class When_searching_for_model_by_name : ModelTestBase
    {
        private It Should_return_the_model = () => Model.FindElementByName(ModelName).ShouldEqual(Model);
    }
}