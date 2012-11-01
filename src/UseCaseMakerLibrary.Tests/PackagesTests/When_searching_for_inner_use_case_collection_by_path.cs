using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.PackagesTests
{
    [Subject(typeof(Packages))]
    [Ignore("This test cannot pass due to the way paths are handled for collections")]
    public class When_searching_for_inner_use_case_collection_by_path : PackagesTestBase
    {
        private It Should_return_the_inner_use_case_collection =
            () =>
            PackageContainer.FindElementByPath(InnerPackage.UseCases.Path).ShouldEqual(InnerPackage.UseCases);
    }
}