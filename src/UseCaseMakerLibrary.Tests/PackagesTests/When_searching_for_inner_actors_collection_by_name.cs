using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.PackagesTests
{
    [Subject(typeof(Packages))]
    public class When_searching_for_inner_actors_collection_by_name : PackagesTestBase
    {
        private It Should_return_the_inner_actor_collection =
            () => PackageContainer.FindElementByName(InnerPackage.Actors.Name).ShouldEqual(InnerPackage.Actors);
    }
}