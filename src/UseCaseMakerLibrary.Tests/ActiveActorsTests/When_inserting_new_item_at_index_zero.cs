using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ActiveActorsTests
{
    [Subject(typeof(ActiveActors))]
    public class When_inserting_new_item_at_index_zero : ActiveActorsTestBase
    {
        private Because Of = () =>
            {
                _newItem = new ActiveActor();
                ActiveActors.Insert(0, _newItem);
            };

        private It Should_have_new_item_at_index_zero = () => ActiveActors[0].ShouldEqual(_newItem);

        private It Should_have_old_item_at_index_one = () => ActiveActors[1].ShouldEqual(ActiveActor);

        private static ActiveActor _newItem;
    }
}