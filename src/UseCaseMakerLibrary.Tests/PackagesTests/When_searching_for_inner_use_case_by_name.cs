using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.PackagesTests
{
    [Subject(typeof(Packages))]
    public class When_searching_for_inner_use_case_by_name : PackagesTestBase
    {
        private It Should_return_the_inner_use_case =
            () => PackageContainer.FindElementByName(UseCase.Name).ShouldEqual(UseCase);
    }
}