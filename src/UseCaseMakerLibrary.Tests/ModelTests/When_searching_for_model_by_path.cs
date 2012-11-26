using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ModelTests
{
    [Subject(typeof (Model))]
    public class When_searching_for_model_by_path : ModelTestBase
    {
        private It Should_return_the_model = () => Model.FindElementByPath(Model.Prefix + Model.Id).ShouldEqual(Model);
    }
}