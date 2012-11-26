using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.UseCaseTests
{
    [Subject(typeof(UseCase))]
    public class When_finding_step_by_unique_id : UseCaseTestBase
    {
        private Establish Context = () =>
            {
                UseCase.AddStep(null, Step.StepType.Default, "", null, DependencyItem.ReferenceType.None);
                _stepToFind = (Step)UseCase.Steps[0];
            };

        private It Should_return_correct_step =
            () => UseCase.FindStepByUniqueID(_stepToFind.UniqueId).ShouldEqual(_stepToFind);
        
        private static Step _stepToFind;
    }
}