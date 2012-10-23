using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.UseCaseTests
{
    [Subject(typeof(UseCase))]
    public class When_testing_if_alternative_child_step_has_alternatives : UseCaseTestBase
    {
        private Establish Context = () =>
            {
                UseCase.AddStep(null, Step.StepType.AlternativeChild, "", null, DependencyItem.ReferenceType.None);
                _stepToTest = (Step)UseCase.Steps[0];
                _stepToTest.Type = Step.StepType.AlternativeChild;
            };

        private It Should_return_true = () => UseCase.StepHasAlternatives(_stepToTest).ShouldBeTrue();

        private static Step _stepToTest;
    }
}