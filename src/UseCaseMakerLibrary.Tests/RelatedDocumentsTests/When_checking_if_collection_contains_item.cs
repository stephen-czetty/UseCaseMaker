using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.RelatedDocumentsTests
{
    [Subject(typeof(RelatedDocuments))]
    public class When_checking_if_collection_contains_item : RelatedDocumentsTestBase
    {
        private It Should_return_true_if_item_exists = () => RelatedDocuments.Contains(RelatedDocument).ShouldBeTrue();

        private It Should_return_false_if_item_does_not_exist =
            () => RelatedDocuments.Contains(new RelatedDocument()).ShouldBeFalse();
    }
}