using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.PackagesTests
{
    [Subject(typeof(Packages))]
    public class When_searching_for_inner_actor_by_unique_id : PackagesTestBase
    {
        private It Should_return_the_inner_actor =
            () => PackageContainer.FindElementByUniqueID(Actor.UniqueID).ShouldEqual(Actor);
    }
}