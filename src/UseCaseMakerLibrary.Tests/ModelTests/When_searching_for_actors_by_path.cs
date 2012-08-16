using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ModelTests
{
    // TODO: This test fails, and it doesn't look like there is a way to make it pass given the current
    // state of the code.  IdentificableObjectCollection.Path returns the parent object's path, so Model.FindElementByPath(Model.Actors)
    // will always return Model, not Model.Actors.
    [Subject(typeof (Model))]
    public class When_searching_for_actors_by_path : ModelTestBase
    {
        private Because Of = () =>
                                 {
                                     _path = Model.Prefix + Model.ID + "." + Model.Actors.Prefix + Model.Actors.ID;
                                 };

        private It Should_return_the_actors = () => Model.FindElementByPath(_path).ShouldEqual(Model.Actors);
        private static string _path;
    }
}