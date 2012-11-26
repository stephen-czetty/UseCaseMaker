using Machine.Specifications;
using UMMO.TestingUtils;

namespace UseCaseMakerLibrary.Tests.PackageTests
{
    [Subject(typeof (Package))]
    public class When_creating_a_new_package : PackageTestBase
    {
        private Because Of = () =>
                                 {
                                     _packageName = A.Random.String.Resembling.A.FirstName;
                                     _packagePrefix = A.Random.String;
                                     _packageId = A.Random.Integer;
                                     _package = Package.NewPackage(_packageName, _packagePrefix, _packageId);
                                 };

        private It Should_not_return_a_null_package = () => _package.ShouldNotBeNull();

        private It Should_set_package_name_correctly = () => _package.Name.ShouldEqual(_packageName);
        private It Should_set_package_prefix_correctly = () => _package.Prefix.ShouldEqual(_packagePrefix);
        private It Should_set_package_id_correctly = () => _package.Id.ShouldEqual(_packageId);


        private static string _packageName;
        private static string _packagePrefix;
        private static int _packageId;
        private static Package _package;
    }
}