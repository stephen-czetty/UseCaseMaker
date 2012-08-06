using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ModelTests
{
    // General Issues:
    // * All of the different classes tested here are essentially the same thing
    // * No abstractions available, so we are forced to artificially populate items for test
    // * These tests are likely to be fragile, so they may start failing as we refactor
    // * Model uses ArrayList instead of IList<T>
    // * Not testing the other constructors for Model
    // * Some methods return index values, but provide no (obvious) way to use that value.  Appears to be
    //   only useful to the UI.
    [Subject("Model tests")]
    public abstract class ModelTestBase
    {
        private Establish Context = () => Model = new Model();
        protected static Model Model;
    }

    [Subject(typeof(Model))]
    public class When_searching_for_element_with_models_unique_id : ModelTestBase
    {
        private It Should_return_the_model = () => Model.FindElementByUniqueID(Model.UniqueID).ShouldEqual(Model);
    }

    [Subject(typeof(Model))]
    public class When_searching_for_element_contained_in_model : ModelTestBase
    {
        // ISSUE: Dependency on two classes
        private Because Of = () =>
        {
            _element = new GlossaryItem();
            Model.AddGlossaryItem(_element);
        };

        private It Should_return_the_element =
            () => Model.FindElementByUniqueID(_element.UniqueID).ShouldEqual(_element);

        private static GlossaryItem _element;
    }

    [Subject(typeof(Model))]
    public class When_searching_for_requirements_element_contained_in_model : ModelTestBase
    {
        // ISSUE: API is clunky
        private It Should_return_the_requirement =
            () => Model.FindElementByUniqueID(Model.Requirements.UniqueID).ShouldEqual(Model.Requirements);
    }

    [Subject(typeof(Model))]
    public class When_searching_for_actors_element_contained_in_model : ModelTestBase
    {
        // ISSUE: API is clunky
        private It Should_return_the_actors =
            () => Model.FindElementByUniqueID(Model.Actors.UniqueID).ShouldEqual(Model.Actors);
    }

    [Subject(typeof(Model))]
    public class When_searching_for_individual_actor_element_contained_in_model : ModelTestBase
    {
        // ISSUE: Dependency on two classes
        // ISSUE: Actor class does not assign its own ID
        private Because Of = () =>
                                 {
                                     _actor = new Actor {ID = 1};
                                     Model.AddActor(_actor);
                                 };

        private It Should_return_the_specific_actor =
            () => Model.FindElementByUniqueID(_actor.UniqueID).ShouldEqual(_actor);
        

        private static Actor _actor;
    }

    [Subject(typeof (Model))]
    public class When_searching_for_usecases_elment_contained_in_model : ModelTestBase
    {
        // ISSUE: API is clunky
        private It Should_return_the_use_cases =
            () => Model.FindElementByUniqueID(Model.UseCases.UniqueID).ShouldEqual(Model.UseCases);
    }

    [Subject(typeof (Model))]
    public class When_searching_for_individual_use_case_contained_in_model : ModelTestBase
    {
        // ISSUE: Dependency on two classes
        private Because Of = () =>
                                 {
                                     _useCase = new UseCase();
                                     Model.AddUseCase(_useCase);
                                 };

        private It Should_return_the_specific_use_case = () => Model.FindElementByUniqueID(_useCase.UniqueID).ShouldEqual(_useCase);
      
        private static UseCase _useCase;
    }

    [Subject(typeof (Model))]
    public class When_searching_for_package : ModelTestBase
    {
        // ISSUE: Dependency on two classes
        private Because Of = () =>
                                 {
                                     _package = new Package();
                                     Model.AddPackage(_package);
                                 };

        private It Should_return_correct_package =
            () => Model.FindElementByUniqueID(_package.UniqueID).ShouldEqual(_package);
        

        private static Package _package;
    }
}