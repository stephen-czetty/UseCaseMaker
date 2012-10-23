using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.UseCaseTests
{
    [Behaviors]
    public class StepCreationBehavior : StepCreationTestBase
    {
        private It Should_set_the_step_stereotype =
            () => ((Step)UseCase.Steps[StepIndex]).Dependency.Stereotype.ShouldEqual(Stereotype);

        private It Should_set_the_step_partner_unique_id_to_the_referenced_use_case =
            () => ((Step)UseCase.Steps[StepIndex]).Dependency.PartnerUniqueID.ShouldEqual(OtherUseCase.UniqueID);

        private It Should_set_the_step_type_correctly =
            () => ((Step)UseCase.Steps[StepIndex]).Dependency.Type.ShouldEqual(DependencyItem.ReferenceType.Client);

        private It Should_set_the_description =
            () =>
            ((Step)UseCase.Steps[StepIndex]).Description.ShouldEqual(
                "<<" + Stereotype + ">> \"" + OtherUseCaseName + "\"");

        private It Should_set_the_step_id_to_one = () => ((Step)UseCase.Steps[0]).ID.ShouldEqual(1);
    }
}