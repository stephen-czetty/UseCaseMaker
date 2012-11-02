using System;
using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.RelatedDocumentsTests
{
    [Subject(typeof(RelatedDocuments))]
    public class When_adding_null_related_document : RelatedDocumentsTestBase
    {
        private It Should_throw_argument_null_exception = () => Catch.Exception(() => RelatedDocuments.Add(null)).ShouldBeOfType<ArgumentNullException>();
    }
}