using Machine.Fakes;
using Machine.Specifications;
using UseCaseMakerLibrary.Contracts;
using GivenIt = Moq.It;

namespace UseCaseMakerLibrary.Tests.Services.ModelRepository
{
    [Subject(typeof(UseCaseMakerLibrary.Services.ModelRepository))]
    public class When_creating_new_model : WithSubject<UseCaseMakerLibrary.Services.ModelRepository>
    {
        private Establish Context = () => The<ILocalizationService>().WhenToldTo(
            x => x.GetValue(GivenIt.IsAny<string>(), GivenIt.IsAny<string>())).Return("NewModel");

        private Because Of = () => _model = Subject.CreateNewModel();

        private It Should_not_return_null = () => _model.ShouldNotBeNull();

        private It Should_set_prefix_to_m = () => _model.Prefix.ShouldEqual("M");

        private It Should_set_name_correctly = () => _model.Name.ShouldEqual("NewModel");

        private static IModel _model;
    }
}