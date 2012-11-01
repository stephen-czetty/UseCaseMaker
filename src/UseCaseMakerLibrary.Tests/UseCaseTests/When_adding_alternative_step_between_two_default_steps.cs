using Machine.Specifications;

using UseCaseMakerLibrary.Tests.Behaviors;

namespace UseCaseMakerLibrary.Tests.UseCaseTests
{
    [Subject(typeof(UseCase))]
    public class When_adding_alternative_step_between_two_default_steps : UseCaseTestBase
    {
        private Establish Context = () =>
            {
                UseCase.AddStep(null, Step.StepType.Default, "", null, DependencyItem.ReferenceType.None);
                _previousStep = (Step)UseCase.Steps[0];
                UseCase.AddStep(_previousStep, Step.StepType.Default, "", null, DependencyItem.ReferenceType.None);
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

        private It Should_set_step_type_to_alternative = () => ((Step)UseCase.Steps[StepIndex]).Type.ShouldEqual(Step.StepType.Alternative);

        private It Should_add_step_to_end_of_list = () => StepIndex.ShouldEqual(1);

        private It Should_set_step_prefix_to_a = () => ((Step)UseCase.Steps[StepIndex]).Prefix.ShouldEqual("A");

        private static Step _previousStep;
    }
}