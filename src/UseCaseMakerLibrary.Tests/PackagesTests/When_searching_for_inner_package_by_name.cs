using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.PackagesTests
{
    [Subject(typeof(Packages))]
    public class When_searching_for_inner_package_by_name : PackagesTestBase
    {
        private It Should_return_the_inner_package =
            () => PackageContainer.FindElementByName(InnerPackage.Name).ShouldEqual(InnerPackage);
    }
}