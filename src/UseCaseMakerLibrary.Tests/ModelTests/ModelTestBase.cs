using Machine.Specifications;
//using UMMO.TestingUtils;

namespace UseCaseMakerLibrary.Tests.ModelTests
{
    // General Issues:
    // * All of the different classes tested here are essentially the same thing
    // * No abstractions available, so we are forced to artificially populate items for test
    // * These tests are likely to be fragile, so they may start failing as we refactor
    // * Model uses ArrayList instead of IList<T>
    // * Not testing the other constructors for Model
    // * Some methods return index values, but provide no (obvious) way to use that value.  Appears to be
    //   only useful to the UI.
    [Subject("Model tests")]
    public abstract class ModelTestBase
    {
        private Establish Context = () =>
                                        {
                                            //ModelName = A.Random.String;
                                            Model = new Model { Name = ModelName };
                                        };
        protected static Model Model;
        protected static string ModelName;
    }
}