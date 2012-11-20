using Machine.Specifications;
using UMMO.TestingUtils.RandomData;

namespace UseCaseMakerLibrary.Tests.ModelTests
{
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
            () => Model.FindElementByUniqueId(_element.UniqueID).ShouldEqual(_element);

        private It Should_return_the_element_from_get_glossary_item =
            () => Model.GetGlossaryItem(_element.UniqueID).ShouldEqual(_element);
        

        private static GlossaryItem _element;
    }
}