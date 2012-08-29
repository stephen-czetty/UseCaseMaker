using Machine.Specifications;
using UMMO.TestingUtils;

namespace UseCaseMakerLibrary.Tests.ModelTests
{
    [Subject(typeof (Model))]
    public class When_searching_for_individual_actor_by_name : ModelTestBase
    {
        private Because Of = () =>
                                 {
                                     _randomName = A.Random.String;
                                     _actor = new Actor {ID = 1, Name = _randomName};
                                     Model.AddActor(_actor);
                                 };

        private It Should_return_the_specific_actor = () => Model.FindElementByName(_randomName).ShouldEqual(_actor);
        

        private static Actor _actor;
        private static string _randomName;
    }
}