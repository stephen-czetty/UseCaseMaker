using System.IO;
using Machine.Fakes;
using Machine.Specifications;
using UseCaseMakerLibrary.Contracts;
using GivenIt = Moq.It;

namespace UseCaseMakerLibrary.Tests.Services.SerializedSaveDataService
{
    [Subject(typeof(UseCaseMakerLibrary.Services.SerializedSaveDataService))]
    public class When_loading_from_saved_data_service : WithSubject<UseCaseMakerLibrary.Services.SerializedSaveDataService>
    {
        private Establish Context = () =>
            {
                The<IXmlSerializerSelector>().WhenToldTo(x => x.GetSerializerForInputFile(GivenIt.IsAny<string>()))
                    .Return(The<ISerializer<Model>>);
                The<ISerializer<Model>>().WhenToldTo(x => x.DeSerialize(GivenIt.IsAny<TextReader>()))
                    .Return(new Model());
            };

        private Because Of = () => Subject.Load(Path.GetTempFileName());

        private It Should_call_serializer_selector = () => The<IXmlSerializerSelector>().WasToldTo(x => x.GetSerializerForInputFile(GivenIt.IsAny<string>()));

        private It Should_call_deserialize = () => The<ISerializer<Model>>().WasToldTo(x => x.DeSerialize(GivenIt.IsAny<TextReader>()));
    }
}