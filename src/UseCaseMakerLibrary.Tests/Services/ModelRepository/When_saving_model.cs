using Machine.Fakes;
using Machine.Specifications;
using UseCaseMakerLibrary.Contracts;

namespace UseCaseMakerLibrary.Tests.Services.ModelRepository
{
    [Subject(typeof(UseCaseMakerLibrary.Services.ModelRepository))]
    public class When_saving_model : WithSubject<UseCaseMakerLibrary.Services.ModelRepository>
    {
        private Establish Context = () => { _model = new Model(); };

        private Because Of = () => Subject.SaveModel(_model, "");

        private It Should_call_save = () => The<ISavedDataService>().WasToldTo(x => x.Save(_model, ""));

        private static Model _model;
    }
}