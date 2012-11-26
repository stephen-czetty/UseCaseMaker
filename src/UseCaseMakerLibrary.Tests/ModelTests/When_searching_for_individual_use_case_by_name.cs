using Machine.Specifications;
using UMMO.TestingUtils;

namespace UseCaseMakerLibrary.Tests.ModelTests
{
    [Subject(typeof (Model))]
    public class When_searching_for_individual_use_case_by_name : ModelTestBase
    {
        private Because Of = () =>
                                 {
                                     _randomName = A.Random.String;
                                     _useCase = new UseCase {Name = _randomName};
                                     Model.AddUseCase(_useCase);
                                 };

        private It Should_return_the_specific_use_case =
            () => Model.FindElementByName(_randomName).ShouldEqual(_useCase);
        

        private static string _randomName;
        private static UseCase _useCase;
    }

    [Subject(typeof (Model))]
    public class When_searching_for_individual_use_case_by_path : ModelTestBase
    {
        private Because Of = () =>
                                 {
                                     var id = A.Random.Integer;
                                     _useCase = new UseCase {Id = id};
                                     Model.AddUseCase(_useCase);
                                     _path = Model.Prefix + Model.Id + "." + _useCase.Prefix + _useCase.Id;
                                 };

        private It Should_return_the_specific_use_case = () => Model.FindElementByPath(_path).ShouldEqual(_useCase);
        

        private static UseCase _useCase;
        private static string _path;
    }
}