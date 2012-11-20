using System.IO;
using Machine.Fakes;
using Machine.Specifications;
using UseCaseMakerLibrary.Contracts;
using GivenIt = Moq.It;

namespace UseCaseMakerLibrary.Tests.Services.SerializedSaveDataService
{
    [Subject(typeof(UseCaseMakerLibrary.Services.SerializedSaveDataService))]
    public class When_saving_to_saved_data_service : WithSubject<UseCaseMakerLibrary.Services.SerializedSaveDataService>
    {
        private Because Of = () => Subject.Save(new Model(), Path.GetTempFileName());

        private It Should_call_serialize = () => The<ISerializer<IModel>>().WasToldTo(x => x.Serialize(GivenIt.IsAny<Model>(), GivenIt.IsAny<TextWriter>()));
    }
}