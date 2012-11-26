using Machine.Specifications;
using UMMO.TestingUtils;

namespace UseCaseMakerLibrary.Tests.ModelTests
{
    [Subject(typeof (Model))]
    public class When_searching_for_package_by_path : ModelTestBase
    {
        private Because Of = () =>
                                 {
                                     _package = new Package {Id = A.Random.Integer};
                                     Model.AddPackage(_package);
                                     _path = Model.Prefix + Model.Id + "." + _package.Prefix + _package.Id;
                                 };

        private It Should_return_the_correct_package = () => Model.FindElementByPath(_path).ShouldEqual(_package);
        

        private static Package _package;
        private static string _path;
    }
}