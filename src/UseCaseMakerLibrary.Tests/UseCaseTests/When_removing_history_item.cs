using System;
using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.UseCaseTests
{
    [Subject(typeof(UseCase))]
    public class When_removing_history_item : UseCaseTestBase
    {
        private Establish Context = () =>
            {
                UseCase.AddHistoryItem(DateTime.Now, HistoryItem.HistoryType.Status, 0, "");
                UseCase.HistoryItems.Count.ShouldEqual(1);
            };

        private Because Of = () => UseCase.RemoveHistoryItem(0);

        private It Should_remove_the_item = () => UseCase.HistoryItems.Count.ShouldEqual(0);
    }
}