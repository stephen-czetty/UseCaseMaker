using System.Globalization;
using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.HistoryItemTests
{
    [Subject(typeof(HistoryItem))]
    public class When_retrieving_date_value : HistoryItemTestBase
    {
        private It Should_return_date_string_localized_to_invariant_culture =
            () => HistoryItem.DateValue.ShouldEqual(TestDate.ToString(DateTimeFormatInfo.InvariantInfo));
    }
}