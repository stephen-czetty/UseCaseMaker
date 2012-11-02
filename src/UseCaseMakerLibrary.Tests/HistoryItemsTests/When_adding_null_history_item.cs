using System;
using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.HistoryItemsTests
{
    [Subject(typeof(HistoryItems))]
    public class When_adding_null_history_item : HistoryItemsTestBase
    {
        private It Should_throw_argument_null_exception = () => Catch.Exception(() => HistoryItems.Add(null)).ShouldBeOfType<ArgumentNullException>();
    }
}