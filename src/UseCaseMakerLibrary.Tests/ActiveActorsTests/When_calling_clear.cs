using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ActiveActorsTests
{
    [Subject(typeof(ActiveActors))]
    public class When_calling_clear : ActiveActorsTestBase
    {
        private Because Of = () => ActiveActors.Clear();

        private It Should_clear_all_items_from_the_list = () => ActiveActors.Count.ShouldEqual(0);
    }
}