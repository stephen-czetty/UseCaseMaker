using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.PackagesTests
{
    [Subject(typeof(Packages))]
    public class When_searching_for_inner_use_case_by_path : PackagesTestBase
    {
        private It Should_return_the_inner_use_case =
            () => PackageContainer.FindElementByPath(UseCase.Path).ShouldEqual(UseCase);
    }
}