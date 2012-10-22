using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ActorsTests
{
    [Subject("Actors Tests")]
    public abstract class ActorsTestBase
    {
        private Establish Context = () =>
            {
                Package = new Package();
                Actors = new Actors(Package);
            };

        protected static Package Package;

        protected static Actors Actors;
    }
}