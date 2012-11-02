using System;
using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.HistoryItemsTests
{
    [Subject(typeof(HistoryItems))]
    public class When_adding_non_history_item : HistoryItemsTestBase
    {
        private It Should_throw_argument_exception = () => Catch.Exception(() => HistoryItems.Add(new Model())).ShouldBeOfType<ArgumentException>(); 
    }
}