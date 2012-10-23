using Machine.Specifications;
using UseCaseMakerLibrary.Tests.Behaviors;

namespace UseCaseMakerLibrary.Tests.UseCaseTests
{
    [Subject(typeof(UseCase))]
    public class When_inserting_a_step_before_default_step : UseCaseTestBase
    {
        private Establish Context = () =>
            {
                UseCase.AddStep(null, Step.StepType.Default, "", null, DependencyItem.ReferenceType.None);
                _previousStep = (Step)UseCase.Steps[0];
            };

        private Because Of =
            () =>
            StepIndex = UseCase.InsertStep(_previousStep, Stereotype, OtherUseCase, DependencyItem.ReferenceType.Client);

        private Behaves_like<StepCreationBehavior> a_step_creator;

        private It Should_insert_step = () => UseCase.Steps.Count.ShouldEqual(2);

        private It Should_place_new_step_before_existing_step = () => StepIndex.ShouldEqual(0);

        private static Step _previousStep;
    }
}