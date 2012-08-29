using Machine.Specifications;
using UMMO.TestingUtils;

namespace UseCaseMakerLibrary.Tests.ModelTests
{
    [Subject(typeof (Model))]
    public class When_searching_for_usecases_by_name : ModelTestBase
    {
        private Because Of = () =>
                                 {
                                     _useCaseName = A.Random.String.Resembling.A.Noun;
                                     Model.UseCases.Name = _useCaseName;
                                 };

        private It Should_return_the_use_cases = () => Model.FindElementByName(_useCaseName).ShouldEqual(Model.UseCases);
        

        private static string _useCaseName;
    }
}