using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.UseCaseTests
{
    [Subject(typeof(UseCase))]
    public class When_searching_for_open_issue_by_unique_id : UseCaseTestBase
    {
        private Establish Context = () =>
            {
                UseCase.AddOpenIssue();
                UseCase.AddOpenIssue();
                _openIssue = (OpenIssue)UseCase.OpenIssues[0];
            };

        private It Should_return_correct_open_issue =
            () => UseCase.FindOpenIssueByUniqueID(_openIssue.UniqueID).ShouldEqual(_openIssue);

        private static OpenIssue _openIssue;
    }
}