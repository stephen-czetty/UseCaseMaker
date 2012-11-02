using System;
using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.RelatedDocumentsTests
{
    [Subject(typeof(RelatedDocuments))]
    public class When_adding_non_related_document : RelatedDocumentsTestBase
    {
        private It Should_throw_argument_exception = () => Catch.Exception(() => RelatedDocuments.Add(new Model())).ShouldBeOfType<ArgumentException>();
    }
}