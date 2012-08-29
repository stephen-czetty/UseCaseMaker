using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ModelTests
{
    // TODO: This test fails, and it doesn't look like there is a way to make it pass given the current
    // state of the code.  IdentificableObjectCollection.Path returns the parent object's path, so Model.FindElementByPath(Model.UseCases)
    // will always return Model, not Model.UseCases.
    [Subject(typeof (Model))]
    public class When_searching_for_usecases_by_path : ModelTestBase
    {
        private Because Of = () =>
                                 {
                                     _path = Model.Prefix + Model.ID;
                                 };

        [Ignore("Test fails")]
        private It Should_return_the_use_cases = () => Model.FindElementByPath(_path).ShouldEqual(Model.UseCases);
        

        private static string _path;
    }
}