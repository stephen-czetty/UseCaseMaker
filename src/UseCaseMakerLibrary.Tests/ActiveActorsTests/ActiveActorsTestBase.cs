using Machine.Specifications;
using UseCaseMakerLibrary.Tests.Behaviors;

namespace UseCaseMakerLibrary.Tests.ActiveActorsTests
{
    [Subject("ActiveActors Tests")]
    public abstract class ActiveActorsTestBase : CollectionTestBase
    {
        private Establish Context = () =>
            {
                ActiveActor = new ActiveActor();
                ActiveActors = new ActiveActors { ActiveActor };
                ExpectedCount = 1;
            };

        protected static ActiveActor ActiveActor;

        protected static ActiveActors ActiveActors
        {
            get
            {
                return (ActiveActors)Collection;
            }

            set
            {
                Collection = value;
            }
        }
    }
}