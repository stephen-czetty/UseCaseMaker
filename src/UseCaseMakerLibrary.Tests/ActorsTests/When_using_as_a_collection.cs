using Machine.Specifications;

using UseCaseMakerLibrary.Tests.Behaviors;

namespace UseCaseMakerLibrary.Tests.ActorsTests
{
    [Subject(typeof(Actors))]
    public class When_using_as_a_collection : ActorsTestBase
    {
        private Behaves_like<NonSynchronizedCollectionBehavior<Actor>> a_non_synchronized_collection;
    }
}