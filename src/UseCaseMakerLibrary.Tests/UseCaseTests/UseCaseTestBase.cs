using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.UseCaseTests
{
    [Subject("UseCase Tests")]
    public abstract class UseCaseTestBase
    {
        private Establish Context = () => UseCase = new UseCase();

        protected static UseCase UseCase;
    }
}