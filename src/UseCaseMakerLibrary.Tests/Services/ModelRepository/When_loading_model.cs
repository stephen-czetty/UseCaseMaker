using Machine.Fakes;
using Machine.Specifications;
using UseCaseMakerLibrary.Contracts;

namespace UseCaseMakerLibrary.Tests.Services.ModelRepository
{
    [Subject(typeof(UseCaseMakerLibrary.Services.ModelRepository))]
    public class When_loading_model : WithSubject<UseCaseMakerLibrary.Services.ModelRepository>
    {
        private Because Of = () => Subject.LoadModel("");

        private It Should_call_load = () => The<ISavedDataService>().WasToldTo(x => x.Load(""));
    }
}