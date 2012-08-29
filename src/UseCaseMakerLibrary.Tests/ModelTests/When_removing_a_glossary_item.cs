using Machine.Specifications;

namespace UseCaseMakerLibrary.Tests.ModelTests
{
    [Subject(typeof (Model))]
    public class When_removing_a_glossary_item : ModelTestBase
    {
        private Because Of = () =>
                                 {
                                     _glossaryItem1 = new GlossaryItem { ID = 1 };
                                     _glossaryItem2 = new GlossaryItem { ID = 2 };
                                     Model.AddGlossaryItem(_glossaryItem1);
                                     Model.AddGlossaryItem(_glossaryItem2);

                                     Model.RemoveGlossaryItem(_glossaryItem1, "", "", "", "", false);
                                 };

        private It Should_remove_glossary_item = () => Model.Glossary.ShouldNotContain(_glossaryItem1);

        private It Should_reset_glossary_item_2_id_to_one =
            () => ((GlossaryItem)Model.Glossary[0]).ID.ShouldEqual(1);
        

        private static GlossaryItem _glossaryItem1;
        private static GlossaryItem _glossaryItem2;
    }
}