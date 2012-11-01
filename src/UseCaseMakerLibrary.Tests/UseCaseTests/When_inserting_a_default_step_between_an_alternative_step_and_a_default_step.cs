using Machine.Specifications;

using UseCaseMakerLibrary.Tests.Behaviors;

namespace UseCaseMakerLibrary.Tests.UseCaseTests
{
    [Subject(typeof(UseCase))]
    public class When_inserting_a_default_step_between_an_alternative_step_and_a_default_step : UseCaseTestBase
    {
        private Establish Context = () =>
            {
                UseCase.AddStep(null, Step.StepType.Alternative, "", null, DependencyItem.ReferenceType.None);
                _previousStep = (Step)UseCase.Steps[0];
                UseCase.AddStep(_previousStep, Step.StepType.Default, "", null, DependencyItem.ReferenceType.None);
                _previousStep.Type = Step.StepType.Alternative;
            };

        private Because Of = () => StepIndex = UseCase.AddStep(
            _previousStep, Step.StepType.Default, Stereotype, OtherUseCase, DependencyItem.ReferenceType.Client);

        private It Should_add_step_to_use_case = () => UseCase.Steps.Count.ShouldEqual(3);

        private It Should_insert_case_at_index_one = () => StepIndex.ShouldEqual(1);

        private It Should_set_step_prefix_to_a = () => ((Step)UseCase.Steps[StepIndex]).Prefix.ShouldEqual("A");

        private It Should_set_step_type_to_alternative =
            () => ((Step)UseCase.Steps[StepIndex]).Type.ShouldEqual(Step.StepType.Alternative);

        private Behaves_like<StepCreationBehavior> a_step_creator;

        private static Step _previousStep;
    }
}