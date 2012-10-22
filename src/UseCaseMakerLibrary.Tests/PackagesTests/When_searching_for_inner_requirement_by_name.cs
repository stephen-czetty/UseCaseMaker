using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.PackagesTests
{
    [Subject(typeof(Packages))]
    [Ignore("This test cannot pass due to the hiding of the Name property in the Requirement class")]
    public class When_searching_for_inner_requirement_by_name : PackagesTestBase
    {
        private It Should_return_the_inner_requirement =
            () => PackageContainer.FindElementByName(Requirement.Name).ShouldEqual(Requirement);
    }
}