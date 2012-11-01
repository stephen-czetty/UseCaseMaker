using System;
using Machine.Specifications;
using UMMO.TestingUtils;

namespace UseCaseMakerLibrary.Tests.UseCaseTests
{
    [Subject(typeof(UseCase))]
    public class When_adding_history_item : UseCaseTestBase
    {
        private Establish Context = () =>
            {
                _date = A.Random.DateTime;
                _action = A.Random.Integer;
                _notes = A.Random.String;
            };

        private Because Of =
            () => UseCase.AddHistoryItem(_date, HistoryItem.HistoryType.Status, _action, _notes);

        private It Should_contain_history_item = () => UseCase.HistoryItems.Count.ShouldEqual(1);

        private It Should_set_date_in_history_item =
            () => ((HistoryItem)UseCase.HistoryItems[0]).Date.ShouldEqual(_date);

        private It Should_set_action_in_history_item =
            () => ((HistoryItem)UseCase.HistoryItems[0]).Action.ShouldEqual(_action);

        private It Should_set_notes_in_history_item =
            () => ((HistoryItem)UseCase.HistoryItems[0]).Notes.ShouldEqual(_notes);

        private static DateTime _date;
        private static int _action;
        private static string _notes;
    }
}