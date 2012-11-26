using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.UseCaseTests
{
    [Subject(typeof(UseCase))]
    public class When_adding_first_open_issue : UseCaseTestBase
    {
        private Because Of = () => _issueIndex = UseCase.AddOpenIssue();

        private It Should_create_issue_at_index_zero = () => _issueIndex.ShouldEqual(0);

        private It Should_set_issue_id_to_one = () => ((OpenIssue)UseCase.OpenIssues[_issueIndex]).Id.ShouldEqual(1);

        private static int _issueIndex;
    }
}