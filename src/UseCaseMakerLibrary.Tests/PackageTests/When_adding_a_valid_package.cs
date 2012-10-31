using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.PackageTests
{
    [Subject(typeof (Package))]
    public class When_adding_a_valid_package : PackageTestBase
    {
        private Because Of = () =>
                                 {
                                     _package = new Package("", "", 1);
                                     Package.AddPackage(_package);
                                 };

        private It Should_contain_the_package_in_its_collection = () => Package.Packages.ShouldContain(_package);

        private It Should_set_the_package_owner_to_the_package =
            () => Package.Packages[0].Owner.ShouldEqual(Package);


        private static Package _package;
    }
}