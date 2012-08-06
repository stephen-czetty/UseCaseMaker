using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ModelTests
{
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
}