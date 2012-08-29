using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.PackageTests
{
    [Subject(typeof (Package))]
    public class When_removing_a_use_case_that_is_referenced_by_another_use_case : PackageTestBase
    {
        private Because Of = () =>
                                 {
                                     _useCase1 = new UseCase("UseCase1", "", 1);
                                     Package.AddUseCase(_useCase1);
                                     _useCase2 = new UseCase("UseCase2", "", 2);

                                     _useCase2.AddStep(null, Step.StepType.Default, "", _useCase1,
                                                       DependencyItem.ReferenceType.Client);
                                     Package.AddUseCase(_useCase2);

                                     Package.RemoveUseCase(_useCase1, "", "", "", "", false);
                                 };

        private It Should_remove_the_reference_in_the_second_use_case =
            () => ((Step) _useCase2.Steps[0]).Dependency.PartnerUniqueID.ShouldNotEqual(_useCase1.UniqueID);

        private It Should_remove_the_use_case = () => Package.UseCases.ShouldNotContain(_useCase1);
        

        private static UseCase _useCase1;
        private static UseCase _useCase2;
    }
}