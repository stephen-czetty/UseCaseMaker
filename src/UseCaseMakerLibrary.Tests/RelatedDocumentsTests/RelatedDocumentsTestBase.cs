using Machine.Specifications;

using UseCaseMakerLibrary.Tests.Behaviors;

namespace UseCaseMakerLibrary.Tests.RelatedDocumentsTests
{
    [Subject("Related Documents Tests")]
    public class RelatedDocumentsTestBase : CollectionTestBase
    {
        private Establish Context = () =>
            {
                RelatedDocument = new RelatedDocument();
                RelatedDocuments = new RelatedDocuments { RelatedDocument };
                ExpectedCount = 1;
            };

        protected static RelatedDocument RelatedDocument;

        protected static RelatedDocuments RelatedDocuments
        {
            get
            {
                return (RelatedDocuments)Collection;
            }

            set
            {
                Collection = value;
            }
        }
    }
}