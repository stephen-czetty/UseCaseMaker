using Machine.Specifications;
using UMMO.TestingUtils;

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
                                            ModelName = A.Random.String.Resembling.A.Noun;
                                            Model = new Model { Name = ModelName };
                                        };

        protected static Model Model;
        protected static string ModelName;
    }

    // Fill in data for the Model
    // Don't need this for coverage of Model, but we'll need something like it later for coverage of Package
    //int id = 1;
    //_startTag = A.Random.String.Resembling.A.Noun;
    //_endTag = A.Random.String.Resembling.A.Noun;
    //_name = A.Random.String.Resembling.A.Verb;
    //string fullName = _startTag + _name + _endTag;
    //Model.AddRequrement();
    //var useCase = new UseCase {ID = id++, Prose = fullName + A.Random.String};
    //int idx = useCase.AddOpenIssue();
    //((OpenIssue) useCase.OpenIssues[idx]).Description = fullName +
    //                                                    A.Random.String;
    //idx = useCase.AddStep(null, Step.StepType.Default, A.Random.String, useCase, DependencyItem.ReferenceType.Client);
    //((Step)useCase.Steps[idx]).Description = fullName + A.Random.String;
    //Model.AddUseCase(useCase);
}