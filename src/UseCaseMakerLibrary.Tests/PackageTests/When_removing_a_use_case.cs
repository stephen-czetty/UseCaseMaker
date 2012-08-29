using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.PackageTests
{
    [Subject(typeof(Package))]
    public class When_removing_a_use_case : PackageTestBase
    {
        private Because Of = () =>
                                 {
                                     _useCase = new UseCase("UseCase", "", 1);
                                     Package.AddUseCase(_useCase);
                                     Package.RemoveUseCase(_useCase, "", "", "", "", false);
                                 };

        private It Should_remove_the_use_case = () => Package.UseCases.ShouldNotContain(_useCase);

        private static UseCase _useCase;
    }
}