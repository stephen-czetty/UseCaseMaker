using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ModelTests
{
    [Subject(typeof(Model))]
    public class When_searching_for_individual_actor_element_contained_in_model : ModelTestBase
    {
        // ISSUE: Dependency on two classes
        // ISSUE: Actor class does not assign its own ID
        private Because Of = () =>
                                 {
                                     _actor = new Actor {Id = 1};
                                     Model.AddActor(_actor);
                                 };

        private It Should_return_the_specific_actor =
            () => Model.FindElementByUniqueId(_actor.UniqueId).ShouldEqual(_actor);
        

        private static Actor _actor;
    }
}