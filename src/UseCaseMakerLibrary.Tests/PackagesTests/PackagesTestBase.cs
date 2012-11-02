using Machine.Specifications;

using UMMO.TestingUtils;

namespace UseCaseMakerLibrary.Tests.PackagesTests
{
    [Subject("Packages Tests")]
    public class PackagesTestBase
    {
        private Establish Context = () =>
            {
                Package = new Package { Name = A.Random.String, ID = 1 };
                PackageContainer = new Packages(Package) { Name = A.Random.String, ID = 2 };
                InnerPackage = new Package { Name = A.Random.String, ID = 3, Actors = { Name = A.Random.String, ID = 4 } };
                Actor = new Actor { Name = A.Random.String, ID = 5 };
                InnerPackage.Actors.Add(Actor);
                InnerPackage.UseCases.Name = A.Random.String;
                InnerPackage.UseCases.ID = 6;
                UseCase = new UseCase { Name = A.Random.String, ID = 7 };
                InnerPackage.UseCases.Add(UseCase);
                InnerPackage.Requirements.Name = A.Random.String;
                InnerPackage.Requirements.ID = 8;
                InnerPackage.Requirements.Owner = InnerPackage;
                Requirement = new Requirement { ID = 9 };
                InnerPackage.Requirements.Add(Requirement);
                InnerInnerPackage = new Package { Name = A.Random.String, ID = 10 };
                InnerPackage.Packages.Add(InnerInnerPackage);
                PackageContainer.Add(InnerPackage);
            };

        protected static Package Package;

        protected static Packages PackageContainer;

        protected static Package InnerPackage;

        protected static Actor Actor;

        protected static UseCase UseCase;

        protected static Requirement Requirement;

        protected static Package InnerInnerPackage;
    }
}