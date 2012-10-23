using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.UseCaseTests
{
    [Subject(typeof(UseCase))]
    public class When_adding_the_first_default_step : UseCaseTestBase
    {
        private Because Of =
            () =>
            StepIndex =
            UseCase.AddStep(null, Step.StepType.Default, Stereotype, OtherUseCase, DependencyItem.ReferenceType.Client);

        private It Should_add_step_to_use_case = () => UseCase.Steps.Count.ShouldEqual(1);

        private Behaves_like<StepCreationBehavior> a_step_creator;
    }
}