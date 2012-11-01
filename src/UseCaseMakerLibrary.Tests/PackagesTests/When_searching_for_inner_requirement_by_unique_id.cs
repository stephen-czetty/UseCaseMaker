using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.PackagesTests
{
    [Subject(typeof(Packages))]
    public class When_searching_for_inner_requirement_by_unique_id : PackagesTestBase
    {
        private It Should_return_the_inner_requirement =
            () => PackageContainer.FindElementByUniqueID(Requirement.UniqueID).ShouldEqual(Requirement);
    }
}