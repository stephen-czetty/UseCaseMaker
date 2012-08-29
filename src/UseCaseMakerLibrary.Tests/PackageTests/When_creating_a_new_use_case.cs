using Machine.Specifications;
using UMMO.TestingUtils;

namespace UseCaseMakerLibrary.Tests.PackageTests
{
    [Subject(typeof(Package))]
    public class When_creating_a_new_use_case : PackageTestBase
    {
        private Because Of = () =>
        {
            _useCaseName = A.Random.String.Resembling.A.FirstName;
            _useCasePrefix = A.Random.String;
            _useCaseId = A.Random.Integer;
            _useCase = Package.NewUseCase(_useCaseName, _useCasePrefix, _useCaseId);
        };

        private It Should_not_return_a_null_use_aces = () => _useCase.ShouldNotBeNull();

        private It Should_have_set_use_case_name_correctly = () => _useCase.Name.ShouldEqual(_useCaseName);
        private It Should_set_use_case_prefix_correctly = () => _useCase.Prefix.ShouldEqual(_useCasePrefix);
        private It Should_set_use_case_id_correctly = () => _useCase.ID.ShouldEqual(_useCaseId);


        private static string _useCaseName;
        private static string _useCasePrefix;
        private static int _useCaseId;
        private static UseCase _useCase;
    }
}