using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.PackageTests
{
    [Subject(typeof (Package))]
    public class When_removing_a_package : PackageTestBase
    {
        private Because Of = () =>
                                 {
                                     _package = new Package("Package", "", 1);
                                     Package.AddPackage(_package);
                                     Package.Packages.ShouldContain(_package);
                                     Package.RemovePackage(_package, "", "", "", "", false);
                                 };

        private It Should_remove_the_package = () => Package.Packages.ShouldNotContain(_package);

        private static Package _package;
    }
}