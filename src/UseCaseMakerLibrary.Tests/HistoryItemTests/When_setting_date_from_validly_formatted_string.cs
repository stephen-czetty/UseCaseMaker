using System;
using System.Globalization;
using Machine.Specifications;
using UMMO.TestingUtils;

namespace UseCaseMakerLibrary.Tests.HistoryItemTests
{
    [Subject(typeof(HistoryItem))]
    public class When_setting_date_from_validly_formatted_string : HistoryItemTestBase
    {
        private Establish Context = () =>
            {
                _date = A.Random.DateTime;
                _dateString = _date.ToString(DateTimeFormatInfo.InvariantInfo);
            };

        private Because Of = () => HistoryItem.DateValue = _dateString;

        private It Should_set_the_date_correctly = () => HistoryItem.Date.ShouldEqual(_date);

        private static string _dateString;
        private static DateTime _date;
    }
}