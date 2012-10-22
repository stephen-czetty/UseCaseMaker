using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.PackagesTests
{
    [Subject(typeof(Packages))]
    public class When_searching_for_inner_actor_by_path : PackagesTestBase
    {
        private It Should_return_the_inner_actor =
            () => PackageContainer.FindElementByPath(Actor.Path).ShouldEqual(Actor);
    }
}