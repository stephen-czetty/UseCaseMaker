using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.UseCaseTests
{
    [Subject(typeof(UseCase))]
    public class When_inserting_a_default_step_between_two_existing_default_steps : UseCaseTestBase
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
                    _previousStep, Step.StepType.Default, Stereotype, OtherUseCase, DependencyItem.ReferenceType.Client);
            };

        private It Should_add_step_to_use_case = () => UseCase.Steps.Count.ShouldEqual(3);

        private It Should_insert_case_at_index_one = () => StepIndex.ShouldEqual(1);

        private Behaves_like<StepCreationBehavior> a_step_creator;

        private static Step _previousStep;
    }
}