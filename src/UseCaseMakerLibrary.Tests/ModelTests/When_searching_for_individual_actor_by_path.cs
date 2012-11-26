using Machine.Specifications;
using UMMO.TestingUtils;

namespace UseCaseMakerLibrary.Tests.ModelTests
{
    [Subject(typeof (Model))]
    public class When_searching_for_individual_actor_by_path : ModelTestBase
    {
        // TODO: Should this be Model.Actors.Actor?  This will break if we fix the IdentificableObjectCollection path bug.
        private Because Of = () =>
                                 {
                                     var id = A.Random.Integer;
                                     _actor = new Actor{Id = id};
                                     Model.AddActor(_actor);
                                     _path = Model.Prefix + Model.Id + "." + _actor.Prefix + _actor.Id;
                                 };

        private It Should_return_the_specific_actor = () => Model.FindElementByPath(_path).ShouldEqual(_actor);
        

        private static Actor _actor;
        private static string _path;
    }
}