using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.UseCaseTests
{
    [Subject(typeof(UseCase))]
    public class When_removing_an_alternative_step : UseCaseTestBase
    {
        private Establish Context = () =>
            {
                UseCase.AddStep(
                    null, Step.StepType.Alternative, Stereotype, OtherUseCase, DependencyItem.ReferenceType.Client);
                _stepToRemove = (Step)UseCase.Steps[0];
                _stepToRemove.Type = Step.StepType.Alternative;
                _stepToRemove.Prefix = "A";
                UseCase.AddStep(_stepToRemove, Step.StepType.Alternative, "", null, DependencyItem.ReferenceType.None);
                _alternativeChildStep = (Step)UseCase.Steps[1];
                UseCase.AddStep(_stepToRemove, Step.StepType.Default, "", null, DependencyItem.ReferenceType.None);
                _secondAlternativeStep = (Step)UseCase.Steps[2];
                UseCase.AddStep(_stepToRemove, Step.StepType.Default, "", null, DependencyItem.ReferenceType.None);
                _stepToKeep = (Step)UseCase.Steps[3];
                _stepToKeep.Type = Step.StepType.Default;
                _stepToKeep.Prefix = "";
            };

        private Because Of = () => UseCase.RemoveStep(_stepToRemove);

        private It Should_remove_the_step = () => UseCase.Steps.ShouldNotContain(_stepToRemove);

        private It Should_remove_the_alternative_child_step =
            () => UseCase.Steps.ShouldNotContain(_alternativeChildStep);

        private It Should_keep_the_remaining_step = () => UseCase.Steps.ShouldContain(_stepToKeep);

        private It Should_set_kept_step_id_to_one = () => _stepToKeep.Id.ShouldEqual(1);

        private It Should_keep_second_alternative_step = () => UseCase.Steps.ShouldContain(_secondAlternativeStep);

        private It Should_set_prefix_on_second_alternative_step_to_a =
            () => _secondAlternativeStep.Prefix.ShouldEqual("A");

        private static Step _stepToRemove;
        private static Step _stepToKeep;
        private static Step _alternativeChildStep;
        private static Step _secondAlternativeStep;
    }
}