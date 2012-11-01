using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.UseCaseTests
{
    [Subject(typeof(UseCase))]
    public class When_testing_if_default_step_with_alternative_step_has_alternatives : UseCaseTestBase
    {
        private Establish Context = () =>
            {
                UseCase.AddStep(null, Step.StepType.Default, "", null, DependencyItem.ReferenceType.None);
                _stepToTest = (Step)UseCase.Steps[0];
                UseCase.AddStep(_stepToTest, Step.StepType.Alternative, "", null, DependencyItem.ReferenceType.None);
            };

        private It Should_return_true = () => UseCase.StepHasAlternatives(_stepToTest).ShouldBeTrue();

        private static Step _stepToTest;
    }
}