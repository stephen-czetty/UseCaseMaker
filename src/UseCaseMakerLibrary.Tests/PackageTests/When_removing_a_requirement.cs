using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.PackageTests
{
    [Subject(typeof(Package))]
    public class When_removing_a_requirement : PackageTestBase
    {
        private Because Of = () =>
                                 {
                                     Package.AddRequrement();
                                     _requirement = (Requirement) Package.Requirements[0];
                                     Package.Requirements.ShouldContain(_requirement);
                                     Package.RemoveRequirement(_requirement);
                                 };

        private It Should_remove_the_requirement = () => Package.Requirements.ShouldNotContain(_requirement);

        private static Requirement _requirement;
    }
}