using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ActiveActorsTests
{
    [Subject(typeof(ActiveActors))]
    public class When_removing_item_at_index : ActiveActorsTestBase
    {
        private Because Of = () =>
            {
                ActiveActors.Insert(0, new RelatedDocument());
                ActiveActors.RemoveAt(1);
            };

        private It Should_not_contain_item = () => ActiveActors.ShouldNotContain(ActiveActor);
    }
}