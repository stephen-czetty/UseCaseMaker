using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.UseCaseTests
{
    [Subject(typeof(UseCase))]
    public class When_removing_an_alternative_child_step : UseCaseTestBase
    {
        private Establish Context = () =>
            {
                int idx = UseCase.AddStep(
                    null, Step.StepType.Default, Stereotype, OtherUseCase, DependencyItem.ReferenceType.Client);
                _defaultStep = (Step)UseCase.Steps[idx];
                idx = UseCase.AddStep(_defaultStep, Step.StepType.Alternative, "", null, DependencyItem.ReferenceType.None);
                _alternativeStep = (Step)UseCase.Steps[idx];
                idx = UseCase.AddStep(
                    _alternativeStep, Step.StepType.Alternative, "", null, DependencyItem.ReferenceType.None);
                _stepToRemove = (Step)UseCase.Steps[idx];
                idx = UseCase.AddStep(
                    _alternativeStep, Step.StepType.Alternative, "", null, DependencyItem.ReferenceType.None);
                _stepToKeep = (Step)UseCase.Steps[idx];
                _stepToKeep.ChildID = 2; // TODO: Is this a symptom of a bug, or am I doing this wrong?
            };

        private Because Of = () => UseCase.RemoveStep(_stepToRemove);

        private It Should_remove_the_step = () => UseCase.Steps.ShouldNotContain(_stepToRemove);

        private It Should_keep_the_other_alternative_child = () => UseCase.Steps.ShouldContain(_stepToKeep);

        private It Should_set_the_other_alternative_child_child_id_to_one = () => _stepToKeep.ChildID.ShouldEqual(1);

        private It Should_keep_the_default_step = () => UseCase.Steps.ShouldContain(_defaultStep);

        private It Should_keep_the_alternative_step = () => UseCase.Steps.ShouldContain(_alternativeStep);

        private static Step _defaultStep;
        private static Step _alternativeStep;
        private static Step _stepToRemove;
        private static Step _stepToKeep;
    }
}