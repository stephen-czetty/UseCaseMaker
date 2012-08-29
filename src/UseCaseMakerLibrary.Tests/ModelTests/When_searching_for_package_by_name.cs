using Machine.Specifications;
using UMMO.TestingUtils;

namespace UseCaseMakerLibrary.Tests.ModelTests
{
    [Subject(typeof (Model))]
    public class When_searching_for_package_by_name : ModelTestBase
    {
        private Because Of = () =>
                                 {
                                     _packageName = A.Random.String.Resembling.A.Noun;
                                     _package = new Package {Name = _packageName};
                                     Model.AddPackage(_package);
                                 };

        private It Should_return_the_correct_package = () => Model.FindElementByName(_packageName).ShouldEqual(_package);
        

        private static Package _package;
        private static string _packageName;
    }
}