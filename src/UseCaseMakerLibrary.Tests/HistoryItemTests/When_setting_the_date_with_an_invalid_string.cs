using System;
using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.HistoryItemTests
{
    [Subject(typeof(HistoryItem))]
    public class When_setting_the_date_with_an_invalid_string : HistoryItemTestBase
    {
        private It Should_throw_format_exception =
            () => Catch.Exception(() => HistoryItem.DateValue = "a bad date").ShouldBeOfType<FormatException>();
    }
}