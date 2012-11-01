using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.RelatedDocumentsTests
{
    [Subject(typeof(RelatedDocuments))]
    public class When_calling_clear : RelatedDocumentsTestBase
    {
        private Because Of = () => RelatedDocuments.Clear();

        private It Should_clear_all_items_from_the_list = () => RelatedDocuments.Count.ShouldEqual(0);
    }
}