using System;
using Machine.Specifications;
using UMMO.TestingUtils;

namespace UseCaseMakerLibrary.Tests.HistoryItemTests
{
    [Subject("HistoryItem Tests")]
    public abstract class HistoryItemTestBase
    {
        private Establish Context = () =>
            {
                TestDate = A.Random.DateTime;
                HistoryItem = new HistoryItem { Date = TestDate };
            };

        protected static HistoryItem HistoryItem;
        protected static DateTime TestDate;
    }
}