using Machine.Specifications;
using UMMO.TestingUtils;

namespace UseCaseMakerLibrary.Tests.ModelTests
{
    [Subject(typeof (Model))]
    public class When_searching_for_actors_by_name : ModelTestBase
    {
        private Because Of = () =>
                                 {
                                     _actorsName = A.Random.String;
                                     Model.Actors.Name = _actorsName;
                                 };

        private It Should_return_the_actors = () => Model.FindElementByName(_actorsName).ShouldEqual(Model.Actors);
        private static string _actorsName;
    }
}