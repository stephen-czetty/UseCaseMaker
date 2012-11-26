using Machine.Specifications;
using UMMO.TestingUtils.RandomData;

namespace UseCaseMakerLibrary.Tests.ModelTests
{
    [Subject(typeof (Model))]
    public class When_searching_for_individual_use_case_contained_in_model : ModelTestBase
    {
        // ISSUE: Dependency on two classes
        private Because Of = () =>
                                 {
                                     _useCase = new UseCase();
                                     Model.AddUseCase(_useCase);
                                 };

        private It Should_return_the_specific_use_case = () => Model.FindElementByUniqueId(_useCase.UniqueId).ShouldEqual(_useCase);
      
        private static UseCase _useCase;
    }
}