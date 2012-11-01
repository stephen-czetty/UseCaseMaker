using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ActorTests
{
    [Subject("Actor Tests")]
    public abstract class ActorTestsBase
    {
        private Establish Context = () => { Actor = new Actor(); };

        protected static Actor Actor;
    }
}