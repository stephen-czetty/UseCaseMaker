using Machine.Specifications;
using UMMO.TestingUtils;
using UseCaseMakerLibrary.Tests.Behaviors;

namespace UseCaseMakerLibrary.Tests.UseCaseTests
{
    [Subject("UseCase Tests")]
    public abstract class UseCaseTestBase : StepCreationTestBase
    {
        private Establish Context = () =>
            {
                UseCase = new UseCase();
                Stereotype = A.Random.String;
                OtherUseCaseName = A.Random.String;
                OtherUseCase = new UseCase { Name = OtherUseCaseName };
            };
    }
}
