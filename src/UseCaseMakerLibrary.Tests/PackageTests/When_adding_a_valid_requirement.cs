using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.PackageTests
{
    [Subject(typeof(Package))]
    public class When_adding_a_valid_requirement : PackageTestBase
    {
        private Because Of = () =>
                                 {
                                     _requirementIndex = Package.AddRequrement();
                                 };

        private It Should_contain_a_requirement_in_the_collection = () => Package.Requirements.Count.ShouldEqual(_requirementIndex+1);

        [Ignore("Doesn't do this, but maybe it should?")]
        private It Should_set_the_requirement_owner_to_the_package =
            () => ((Requirement)Package.Requirements[0]).Owner.ShouldEqual(Package);

        // TODO: This test is contrived
        private It Should_return_the_requirement_from_unique_id =
            () =>
            Package.FindRequirementByUniqueID(((Requirement) Package.Requirements[0]).UniqueID).ShouldEqual(
                Package.Requirements[0]);


        private static Package _package;
        private static int _requirementIndex;
    }
}