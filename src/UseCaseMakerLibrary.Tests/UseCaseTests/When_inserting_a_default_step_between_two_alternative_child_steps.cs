using Machine.Specifications;

using UseCaseMakerLibrary.Tests.Behaviors;

namespace UseCaseMakerLibrary.Tests.UseCaseTests
{
    [Subject(typeof(UseCase))]
    public class When_inserting_a_default_step_between_two_alternative_child_steps : UseCaseTestBase
    {
        private Establish Context = () =>
            {
                UseCase.AddStep(null, Step.StepType.AlternativeChild, "", null, DependencyItem.ReferenceType.None);
                _previousStep = (Step)UseCase.Steps[0];
                _previousStep.Type = Step.StepType.AlternativeChild;
                _previousStep.Prefix = "A";
                _previousStep.ChildID = 0;
                UseCase.AddStep(_previousStep, Step.StepType.Default, "", null, DependencyItem.ReferenceType.None);
            };

        private Because Of = () => StepIndex = UseCase.AddStep(
            _previousStep, Step.StepType.Default, Stereotype, OtherUseCase, DependencyItem.ReferenceType.Client);

        private It Should_add_step_to_use_case = () => UseCase.Steps.Count.ShouldEqual(3);

        private It Should_insert_case_at_end_of_steps = () => StepIndex.ShouldEqual(2);

        private It Should_set_step_prefix_to_a = () => ((Step)UseCase.Steps[StepIndex]).Prefix.ShouldEqual("A");

        private It Should_set_child_id_to_two = () => ((Step)UseCase.Steps[StepIndex]).ChildID.ShouldEqual(2);

        private It Should_set_type_to_alternative_child =
            () => ((Step)UseCase.Steps[StepIndex]).Type.ShouldEqual(Step.StepType.AlternativeChild);

        private Behaves_like<StepCreationBehavior> a_step_creator;

        private static Step _previousStep;
    }
}