using Machine.Specifications;

using UseCaseMakerLibrary.Tests.Behaviors;

namespace UseCaseMakerLibrary.Tests.UseCaseTests
{
    [Subject(typeof(UseCase))]
    public class When_adding_alternative_step_after_alternative_step : UseCaseTestBase
    {
        private Establish Context = () =>
            {
                UseCase.AddStep(null, Step.StepType.Default, "", null, DependencyItem.ReferenceType.None);
                UseCase.AddStep(
                    (Step)UseCase.Steps[0], Step.StepType.Alternative, "", null, DependencyItem.ReferenceType.None);
                _previousStep = (Step)UseCase.Steps[1];
                UseCase.AddStep(_previousStep, Step.StepType.Alternative, "", null, DependencyItem.ReferenceType.None);
            };

        private Because Of = () =>
            {
                StepIndex = UseCase.AddStep(
                    _previousStep,
                    Step.StepType.Alternative,
                    Stereotype,
                    OtherUseCase,
                    DependencyItem.ReferenceType.Client);
            };

        private Behaves_like<StepCreationBehavior> a_step_creator;

        private It Should_set_step_type_to_alternative_child =
            () => ((Step)UseCase.Steps[StepIndex]).Type.ShouldEqual(Step.StepType.AlternativeChild);

        private It Should_add_step_to_end_of_list = () => StepIndex.ShouldEqual(2);

        private It Should_set_step_prefix_to_a = () => ((Step)UseCase.Steps[StepIndex]).Prefix.ShouldEqual("A");

        private It Should_set_child_id_to_one = () => ((Step)UseCase.Steps[StepIndex]).ChildID.ShouldEqual(1);

        private static Step _previousStep;
    }
}