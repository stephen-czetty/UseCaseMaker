using System;
using Machine.Specifications;
using UMMO.TestingUtils;

namespace UseCaseMakerLibrary.Tests.PackageTests
{
    [Subject(typeof(Package))]
    public class When_creating_a_new_use_case : PackageTestBase
    {
        private Because Of = () =>
        {
            _useCaseName = A.Random.String.Resembling.A.FirstName;
            _useCasePrefix = A.Random.String;
            _useCaseId = A.Random.Integer;
            _useCase = Package.NewUseCase(_useCaseName, _useCasePrefix, _useCaseId);
        };

        private It Should_not_return_a_null_use_aces = () => _useCase.ShouldNotBeNull();

        private It Should_have_set_use_case_name_correctly = () => _useCase.Name.ShouldEqual(_useCaseName);
        private It Should_set_use_case_prefix_correctly = () => _useCase.Prefix.ShouldEqual(_useCasePrefix);
        private It Should_set_use_case_id_correctly = () => _useCase.ID.ShouldEqual(_useCaseId);


        private static string _useCaseName;
        private static string _useCasePrefix;
        private static int _useCaseId;
        private static UseCase _useCase;
    }

    [Subject(typeof(Package))]
    public class When_adding_a_valid_use_case : PackageTestBase
    {
        private Because Of = () =>
        {
            _useCase = new UseCase("", "", 1);
            Package.AddUseCase(_useCase);
        };

        private It Should_contain_the_actor_in_its_collection = () => Package.UseCases.ShouldContain(_useCase);

        private It Should_set_the_actor_owner_to_the_package =
            () => ((UseCase)Package.UseCases[0]).Owner.ShouldEqual(Package);


        private static UseCase _useCase;
    }
    
    [Subject(typeof(Package))]
    public class When_removing_a_use_case : PackageTestBase
    {
        private Because Of = () =>
        {
            _useCase = new UseCase("UseCase", "", 1);
            Package.AddUseCase(_useCase);
            Package.RemoveUseCase(_useCase, "", "", "", "", false);
        };

        private It Should_remove_the_use_case = () => Package.UseCases.ShouldNotContain(_useCase);

        private static UseCase _useCase;
    }
    
    [Subject(typeof(Package))]
    public class When_removing_a_use_case_that_is_in_sub_packages : PackageTestBase
    {
        private Because Of = () =>
        {
            _useCase = new UseCase("ActorName", "", 1);
            Package.AddUseCase(_useCase);
            var package = new Package();
            package.AddUseCase(_useCase);
            Package.AddPackage(package);

            Package.RemoveUseCase(_useCase, "", "", "", "", false);
        };

        private It Should_not_remove_the_use_case_from_the_sub_package = // Really???
            () => ((Package)Package.Packages[0]).UseCases.ShouldContain(_useCase);

        private It Should_remove_the_use_case = () => Package.UseCases.ShouldNotContain(_useCase);


        private static UseCase _useCase;
    }

    [Subject(typeof (Package))]
    public class When_removing_a_use_case_that_is_referenced_by_another_use_case : PackageTestBase
    {
        private Because Of = () =>
                                 {
                                     _useCase1 = new UseCase("UseCase1", "", 1);
                                     Package.AddUseCase(_useCase1);
                                     _useCase2 = new UseCase("UseCase2", "", 2);

                                     _useCase2.AddStep(null, Step.StepType.Default, "", _useCase1,
                                                       DependencyItem.ReferenceType.Client);
                                     Package.AddUseCase(_useCase2);

                                     Package.RemoveUseCase(_useCase1, "", "", "", "", false);
                                 };

        private It Should_remove_the_reference_in_the_second_use_case =
            () => ((Step) _useCase2.Steps[0]).Dependency.PartnerUniqueID.ShouldNotEqual(_useCase1.UniqueID);

        private It Should_remove_the_use_case = () => Package.UseCases.ShouldNotContain(_useCase1);
        

        private static UseCase _useCase1;
        private static UseCase _useCase2;
    }
}