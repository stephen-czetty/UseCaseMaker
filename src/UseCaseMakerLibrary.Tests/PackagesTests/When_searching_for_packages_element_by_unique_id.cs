using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.PackagesTests
{
    [Subject(typeof(Packages))]
    public class When_searching_for_packages_element_by_unique_id : PackagesTestBase
    {
        private It Should_return_the_package_container = () => PackageContainer.FindElementByUniqueID(PackageContainer.UniqueId).ShouldEqual(PackageContainer); 
    }
}