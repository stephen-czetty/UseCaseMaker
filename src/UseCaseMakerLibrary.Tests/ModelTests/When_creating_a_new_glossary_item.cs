using Machine.Specifications;
using UMMO.TestingUtils;

namespace UseCaseMakerLibrary.Tests.ModelTests
{
    [Subject(typeof (Model))]
    public class When_creating_a_new_glossary_item : ModelTestBase
    {
        private Because Of = () =>
                                 {
                                     _glossaryName = A.Random.String.Resembling.A.Noun;
                                     _glossaryPrefix = A.Random.String.Resembling.A.FirstName;
                                     _glossaryId = A.Random.Integer;
                                     _glossaryItem = Model.NewGlossaryItem(_glossaryName, _glossaryPrefix, _glossaryId);
                                 };

        private It Should_set_the_name_to_glossaryName = () => _glossaryItem.Name.ShouldEqual(_glossaryName);
        private It Should_set_the_prefix_to_glossaryPrefix = () => _glossaryItem.Prefix.ShouldEqual(_glossaryPrefix);
        private It Should_set_the_id_to_glossaryId = () => _glossaryItem.Id.ShouldEqual(_glossaryId);
        private It Should_set_the_owner_to_model = () => _glossaryItem.Owner.ShouldEqual(Model);
        

        private static GlossaryItem _glossaryItem;
        private static string _glossaryName;
        private static string _glossaryPrefix;
        private static int _glossaryId;
    }
}