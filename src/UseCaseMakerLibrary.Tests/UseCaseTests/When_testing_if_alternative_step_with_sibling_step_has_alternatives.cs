using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.UseCaseTests
{
    [Subject(typeof(UseCase))]
    public class When_testing_if_alternative_step_with_sibling_step_has_alternatives : UseCaseTestBase
    {
        private Establish Context = () =>
            {
                UseCase.AddStep(null, Step.StepType.Default, "", null, DependencyItem.ReferenceType.None);
                var defaultStep = (Step)UseCase.Steps[0];
                UseCase.AddStep(defaultStep, Step.StepType.Alternative, "", null, DependencyItem.ReferenceType.None);
                _stepToTest = (Step)UseCase.Steps[1];
                UseCase.AddStep(defaultStep, Step.StepType.Alternative, "", null, DependencyItem.ReferenceType.None);
            };

        private It Should_return_false = () => UseCase.StepHasAlternatives(_stepToTest).ShouldBeFalse();

        private static Step _stepToTest;
    }
}