using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.GoalsTests
{
    [Subject(typeof(Goals))]
    public class When_getting_path_for_root_element
    {
        private Because Of = () => _goals = new Goals();

        private It Should_return_empty_string = () => _goals.Path.ShouldBeEmpty();

        private static Goals _goals;
    }
}