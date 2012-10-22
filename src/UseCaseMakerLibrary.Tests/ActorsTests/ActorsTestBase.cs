using System.Collections;

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
                Actors = new Actors(Package);
            };

        protected static Package Package;

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