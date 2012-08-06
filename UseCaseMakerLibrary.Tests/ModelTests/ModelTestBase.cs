using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ModelTests
{
    [Subject(typeof (Model))]
    public class ModelTestBase
    {
        private Establish Context = () => _model = new Model();
        private static Model _model;

        public class When_searching_for_element_with_models_unique_id
        {
            private It Should_return_the_model = () => _model.FindElementByUniqueID(_model.UniqueID).ShouldEqual(_model);
        }

        public class When_searching_for_element_contained_in_model
        {
            // ISSUE: Dependency on two classes
            private Because Of = () =>
                                     {
                                         _element = new GlossaryItem();
                                         _model.AddGlossaryItem(_element);
                                     };

            private It Should_return_the_element =
                () => _model.FindElementByUniqueID(_element.UniqueID).ShouldEqual(_element);

            private static GlossaryItem _element;
        }
    }
}