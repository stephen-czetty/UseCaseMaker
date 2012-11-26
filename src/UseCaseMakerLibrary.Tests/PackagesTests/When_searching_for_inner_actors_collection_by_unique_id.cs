using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.PackagesTests
{
    [Subject(typeof(Packages))]
    public class When_searching_for_inner_actors_collection_by_unique_id : PackagesTestBase
    {
        private It Should_return_the_inner_actor_collection =
            () => PackageContainer.FindElementByUniqueID(InnerPackage.Actors.UniqueId).ShouldEqual(InnerPackage.Actors);
    }
}