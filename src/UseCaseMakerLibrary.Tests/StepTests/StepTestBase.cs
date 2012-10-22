using Machine.Specifications;

using UMMO.TestingUtils;

namespace UseCaseMakerLibrary.Tests.StepTests
{
    [Subject("Step Tests")]
    public abstract class StepTestBase
    {
        private Establish Context = () => { Step = new Step { Prefix = A.Random.String, ChildID = 2 }; };

        protected static Step Step;
    }
}