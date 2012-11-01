using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.StepTests
{
    [Subject(typeof(Step))]
    public class When_getting_name_from_step : StepTestBase
    {
        private It Should_return_properly_formatted_name = () => Step.Name.ShouldEqual(Step.ID + "." + Step.Prefix + "." + Step.ChildID);
    }
}