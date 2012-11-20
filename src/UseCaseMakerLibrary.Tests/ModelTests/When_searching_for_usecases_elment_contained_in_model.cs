using Machine.Specifications;
using UMMO.TestingUtils.RandomData;

namespace UseCaseMakerLibrary.Tests.ModelTests
{
    [Subject(typeof (Model))]
    public class When_searching_for_usecases_elment_contained_in_model : ModelTestBase
    {
        // ISSUE: API is clunky
        private It Should_return_the_use_cases =
            () => Model.FindElementByUniqueId(Model.UseCases.UniqueID).ShouldEqual(Model.UseCases);
    }
}