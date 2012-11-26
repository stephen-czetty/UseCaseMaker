using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.UseCaseTests
{
    [Subject(typeof(UseCase))]
    public class When_removing_a_default_step : UseCaseTestBase
    {
        private Establish Context = () =>
            {
                UseCase.AddStep(
                    null, Step.StepType.Default, Stereotype, OtherUseCase, DependencyItem.ReferenceType.Client);
                _stepToRemove = (Step)UseCase.Steps[0];
                UseCase.AddStep(_stepToRemove, Step.StepType.Default, "", null, DependencyItem.ReferenceType.None);
                _stepToKeep = (Step)UseCase.Steps[1];
                _stepToKeep.Id.ShouldEqual(2);
            };

        private Because Of = () => UseCase.RemoveStep(_stepToRemove);
        
        private It Should_remove_the_step = () => UseCase.Steps.ShouldNotContain(_stepToRemove);

        private It Should_keep_the_other_step = () => UseCase.Steps.ShouldContain(_stepToKeep);

        private It Should_set_kept_step_id_to_one = () => _stepToKeep.Id.ShouldEqual(1);

        private static Step _stepToRemove;
        private static Step _stepToKeep;
    }
}