using Machine.Specifications;
using UseCaseMakerLibrary.Tests.Behaviors;

namespace UseCaseMakerLibrary.Tests.UseCaseTests
{
    [Subject(typeof(UseCase))]
    public class When_inserting_a_step_before_alternative_child_step : UseCaseTestBase
    {
        private Establish Context = () =>
            { UseCase.AddStep(null, Step.StepType.AlternativeChild, "", null, DependencyItem.ReferenceType.None);
                _previousStep = (Step)UseCase.Steps[0];
                _previousStep.Type = Step.StepType.AlternativeChild;
                _previousStep.Prefix = "A";
                _previousStep.ChildID = 0;
            };

        private Because Of =
            () =>
            StepIndex = UseCase.InsertStep(_previousStep, Stereotype, OtherUseCase, DependencyItem.ReferenceType.Client);

        private Behaves_like<StepCreationBehavior> a_step_creator;

        private It Should_insert_step = () => UseCase.Steps.Count.ShouldEqual(2);

        private It Should_place_new_step_before_existing_step = () => StepIndex.ShouldEqual(0);

        private It Should_set_step_prefix_to_a = () => ((Step)UseCase.Steps[StepIndex]).Prefix.ShouldEqual("A");

        private It Should_keep_previous_step_prefix_at_a = () => _previousStep.Prefix.ShouldEqual("A");

        private It Should_set_child_id_to_zero = () => ((Step)UseCase.Steps[StepIndex]).ChildID.ShouldEqual(0);

        private It Should_set_previous_step_child_id_to_one = () => _previousStep.ChildID.ShouldEqual(1);

        private It Should_set_step_type_to_alternative_child =
            () => ((Step)UseCase.Steps[StepIndex]).Type.ShouldEqual(Step.StepType.AlternativeChild);

        private static Step _previousStep;
    }
}