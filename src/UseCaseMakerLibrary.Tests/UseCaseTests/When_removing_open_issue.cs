using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.UseCaseTests
{
    [Subject(typeof(UseCase))]
    public class When_removing_open_issue : UseCaseTestBase
    {
        private Establish Context = () =>
            {
                UseCase.AddOpenIssue();
                UseCase.AddOpenIssue();
                UseCase.OpenIssues.Count.ShouldEqual(2);
                _remainingIssue = (OpenIssue)UseCase.OpenIssues[1];
                _remainingIssue.ID.ShouldEqual(2);
                _openIssue = (OpenIssue)UseCase.OpenIssues[0];
            };

        private Because Of = () => UseCase.RemoveOpenIssue(_openIssue);

        private It Should_not_contain_the_issue = () => UseCase.OpenIssues.ShouldNotContain(_openIssue);

        private It Should_set_remaining_issue_id_to_one = () => _remainingIssue.ID.ShouldEqual(1);

        private static OpenIssue _openIssue;
        private static OpenIssue _remainingIssue;
    }
}