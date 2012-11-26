using Machine.Specifications;

using UMMO.TestingUtils;

namespace UseCaseMakerLibrary.Tests.PackagesTests
{
    [Subject("Packages Tests")]
    public class PackagesTestBase
    {
        private Establish Context = () =>
            {
                Package = new Package { Name = A.Random.String, Id = 1 };
                PackageContainer = new Packages(Package) { Name = A.Random.String, Id = 2 };
                InnerPackage = new Package { Name = A.Random.String, Id = 3, Actors = { Name = A.Random.String, Id = 4 } };
                Actor = new Actor { Name = A.Random.String, Id = 5 };
                InnerPackage.Actors.Add(Actor);
                InnerPackage.UseCases.Name = A.Random.String;
                InnerPackage.UseCases.Id = 6;
                UseCase = new UseCase { Name = A.Random.String, Id = 7 };
                InnerPackage.UseCases.Add(UseCase);
                InnerPackage.Requirements.Name = A.Random.String;
                InnerPackage.Requirements.Id = 8;
                InnerPackage.Requirements.Owner = InnerPackage;
                Requirement = new Requirement { Id = 9 };
                InnerPackage.Requirements.Add(Requirement);
                InnerInnerPackage = new Package { Name = A.Random.String, Id = 10 };
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