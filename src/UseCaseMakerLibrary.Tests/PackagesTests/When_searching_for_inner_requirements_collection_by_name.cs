using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.PackagesTests
{
    [Subject(typeof(Packages))]
    public class When_searching_for_inner_requirements_collection_by_name : PackagesTestBase
    {
        private It Should_return_the_inner_requirements_collection =
            () =>
            PackageContainer.FindElementByName(InnerPackage.Requirements.Name).ShouldEqual(
                InnerPackage.Requirements);
    }
}