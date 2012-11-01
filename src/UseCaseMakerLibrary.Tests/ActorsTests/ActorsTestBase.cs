using Machine.Specifications;

using UseCaseMakerLibrary.Tests.Behaviors;

namespace UseCaseMakerLibrary.Tests.ActorsTests
{
    [Subject("Actors Tests")]
    public abstract class ActorsTestBase : CollectionTestBase
    {
        private Establish Context = () =>
            {
                Package = new Package();
                Actor = new Actor();
                Actors = new Actors(Package) { Actor };
                ExpectedCount = 1;
            };

        protected static Package Package;

        protected static Actor Actor;

        protected static Actors Actors
        {
            get
            {
                return (Actors)Collection;
            }

            set
            {
                Collection = value;
            }
        }
    }
}