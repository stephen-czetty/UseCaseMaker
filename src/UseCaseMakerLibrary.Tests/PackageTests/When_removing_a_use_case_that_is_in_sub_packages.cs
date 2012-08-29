using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.PackageTests
{
    [Subject(typeof(Package))]
    public class When_removing_a_use_case_that_is_in_sub_packages : PackageTestBase
    {
        private Because Of = () =>
                                 {
                                     _useCase = new UseCase("ActorName", "", 1);
                                     Package.AddUseCase(_useCase);
                                     var package = new Package();
                                     package.AddUseCase(_useCase);
                                     Package.AddPackage(package);

                                     Package.RemoveUseCase(_useCase, "", "", "", "", false);
                                 };

        private It Should_not_remove_the_use_case_from_the_sub_package = // Really???
            () => ((Package)Package.Packages[0]).UseCases.ShouldContain(_useCase);

        private It Should_remove_the_use_case = () => Package.UseCases.ShouldNotContain(_useCase);


        private static UseCase _useCase;
    }
}