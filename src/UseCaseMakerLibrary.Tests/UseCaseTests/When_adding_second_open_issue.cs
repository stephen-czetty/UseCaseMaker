using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.UseCaseTests
{
    [Subject(typeof(UseCase))]
    public class When_adding_second_open_issue : UseCaseTestBase
    {
        private Establish Context = () => UseCase.AddOpenIssue();

        private Because Of = () => _issueIndex = UseCase.AddOpenIssue();

        private It Should_create_index_at_index_one = () => _issueIndex.ShouldEqual(1);

        private It Should_set_issue_id_to_two = () => ((OpenIssue)UseCase.OpenIssues[_issueIndex]).ID.ShouldEqual(2);

        private static int _issueIndex;
    }
}