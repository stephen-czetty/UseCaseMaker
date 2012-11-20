using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ModelTests
{
    [Subject(typeof (Model))]
    public class When_searching_for_package_by_unique_id : ModelTestBase
    {
        // ISSUE: Dependency on two classes
        private Because Of = () =>
                                 {
                                     _package = new Package();
                                     Model.AddPackage(_package);
                                 };

        private It Should_return_correct_package =
            () => Model.FindElementByUniqueId(_package.UniqueID).ShouldEqual(_package);
        

        private static Package _package;
    }
}