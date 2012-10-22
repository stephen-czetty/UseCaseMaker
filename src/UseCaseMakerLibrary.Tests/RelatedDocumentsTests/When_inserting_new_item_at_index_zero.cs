using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.RelatedDocumentsTests
{
    [Subject(typeof(RelatedDocuments))]
    public class When_inserting_new_item_at_index_zero : RelatedDocumentsTestBase
    {
        private Because Of = () =>
            {
                _newItem = new RelatedDocument();
                RelatedDocuments.Insert(0, _newItem);
            };

        private It Should_have_new_item_at_index_zero = () => RelatedDocuments[0].ShouldEqual(_newItem);

        private It Should_have_old_item_at_index_one = () => RelatedDocuments[1].ShouldEqual(RelatedDocument);

        private static RelatedDocument _newItem;
    }
}