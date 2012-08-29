using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.PackageTests
{
    [Subject(typeof(Package))]
    public class When_adding_a_valid_use_case : PackageTestBase
    {
        private Because Of = () =>
                                 {
                                     _useCase = new UseCase("", "", 1);
                                     Package.AddUseCase(_useCase);
                                 };

        private It Should_contain_the_actor_in_its_collection = () => Package.UseCases.ShouldContain(_useCase);

        private It Should_set_the_actor_owner_to_the_package =
            () => ((UseCase)Package.UseCases[0]).Owner.ShouldEqual(Package);


        private static UseCase _useCase;
    }
}