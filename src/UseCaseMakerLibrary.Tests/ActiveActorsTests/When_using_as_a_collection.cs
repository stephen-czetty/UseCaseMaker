using Machine.Specifications;
using UseCaseMakerLibrary.Tests.Behaviors;

namespace UseCaseMakerLibrary.Tests.ActiveActorsTests
{
    [Subject(typeof(ActiveActors))]
    public class When_using_as_a_collection : ActiveActorsTestBase
    {
        private Behaves_like<NonSynchronizedCollectionBehavior> a_collection;
    }
}