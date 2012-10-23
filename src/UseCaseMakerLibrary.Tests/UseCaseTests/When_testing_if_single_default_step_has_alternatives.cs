using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.UseCaseTests
{
    [Subject(typeof(UseCase))]
    public class When_testing_if_single_default_step_has_alternatives : UseCaseTestBase
    {
        private Establish Context = () =>
            {
                UseCase.AddStep(null, Step.StepType.Default, "", null, DependencyItem.ReferenceType.None);
                _stepToTest = (Step)UseCase.Steps[0];
            };

        private It Should_return_false = () => UseCase.StepHasAlternatives(_stepToTest).ShouldBeFalse();

        private static Step _stepToTest;
    }
}