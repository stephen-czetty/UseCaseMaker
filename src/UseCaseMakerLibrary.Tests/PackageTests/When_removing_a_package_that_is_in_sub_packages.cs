using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.PackageTests
{
    [Subject(typeof (Package))]
    public class When_removing_a_package_that_is_in_sub_packages : PackageTestBase
    {
        private Because Of = () =>
                                 {
                                     _package = new Package("ActorName", "", 1);
                                     Package.AddPackage(_package);
                                     var package = new Package();
                                     package.AddPackage(_package);
                                     Package.AddPackage(package);
                                     Package.Packages.ShouldContain(_package);

                                     Package.RemovePackage(_package, "", "", "", "", false);
                                 };

        private It Should_not_remove_the_package_from_the_sub_package = // Really???
            () => ((Package) Package.Packages[0]).Packages.ShouldContain(_package);

        private It Should_remove_the_package = () => Package.Packages.ShouldNotContain(_package);


        private static Package _package;
    }
}