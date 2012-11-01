using Machine.Specifications;

using UseCaseMakerLibrary.Tests.Behaviors;

namespace UseCaseMakerLibrary.Tests.RelatedDocumentsTests
{
    [Subject(typeof(RelatedDocuments))]
    public class When_using_as_a_collection : RelatedDocumentsTestBase
    {
        private Behaves_like<NonSynchronizedCollectionBehavior> a_non_synchronized_collection;
    }
}