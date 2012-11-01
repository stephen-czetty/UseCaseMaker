using System.Globalization;
using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.HistoryItemTests
{
    [Subject(typeof(HistoryItem))]
    public class When_retrieving_localized_date_value_and_only_the_date : HistoryItemTestBase
    {
        private It Should_return_date_string_localized_to_current_culture =
            () => HistoryItem.LocalizatedDateValue.ShouldEqual(TestDate.ToString("d", DateTimeFormatInfo.CurrentInfo));
    }
}