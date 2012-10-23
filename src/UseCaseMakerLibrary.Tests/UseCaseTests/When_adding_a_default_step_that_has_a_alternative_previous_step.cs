using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.UseCaseTests
{
    [Subject(typeof(UseCase))]
    public class When_adding_a_default_step_that_has_a_alternative_previous_step : UseCaseTestBase
    {
        private Establish Context = () =>
            {
                UseCase.AddStep(null, Step.StepType.Alternative, "", null, DependencyItem.ReferenceType.None);
                _previousStep = (Step)UseCase.Steps[0];
                _previousStep.Type = Step.StepType.Alternative;
            };

        private Because Of =
            () =>
            StepIndex =
            UseCase.AddStep(
                _previousStep, Step.StepType.Default, Stereotype, OtherUseCase, DependencyItem.ReferenceType.Client);

        private It Should_add_step_to_use_case = () => UseCase.Steps.Count.ShouldEqual(2);

        private It Should_set_step_prefix_to_a = () => ((Step)UseCase.Steps[StepIndex]).Prefix.ShouldEqual("A");

        private It Should_set_step_type_to_alternative =
            () => ((Step)UseCase.Steps[StepIndex]).Type.ShouldEqual(Step.StepType.Alternative);

        private Behaves_like<StepCreationBehavior> a_step_creator;

        private static Step _previousStep;
    }
}