using System;
using Machine.Specifications;
using UMMO.TestingUtils;

namespace UseCaseMakerLibrary.Tests.PackageTests
{
    [Subject("Package Tests")]
    public class PackageTestBase
    {
        private Establish Context = () => Package = new Package();

        protected static Package Package;
    }

    [Subject(typeof (Package))]
    public class When_instantiating_an_empty_package : PackageTestBase
    {
        private It Should_have_non_null_actors = () => Package.Actors.ShouldNotBeNull();
        private It Should_have_actors_owner_set_to_package = () => Package.Actors.Owner.ShouldEqual(Package);
        
        private It Should_have_non_null_packages = () => Package.Packages.ShouldNotBeNull();
        private It Should_have_packages_owner_set_to_package = () => Package.Actors.Owner.ShouldEqual(Package);
        
        private It Should_have_non_null_use_cases = () => Package.UseCases.ShouldNotBeNull();
        private It Should_have_use_cases_owner_set_to_package = () => Package.UseCases.Owner.ShouldEqual(Package);
        
        private It Should_have_non_null_requirements = () => Package.Requirements.ShouldNotBeNull();
        private It Should_have_non_null_attributes = () => Package.Attributes.ShouldNotBeNull();
    }

    [Subject(typeof (Package))]
    public class When_creating_a_new_actor : PackageTestBase
    {
        private Because Of = () =>
                                 {
                                     _actorName = A.Random.String.Resembling.A.FirstName;
                                     _actorPrefix = A.Random.String;
                                     _actorId = A.Random.Integer;
                                     _actor = Package.NewActor(_actorName, _actorPrefix, _actorId);
                                 };

        private It Should_not_return_a_null_actor = () => _actor.ShouldNotBeNull();
     
        private It Should_have_set_actors_name_correctly = () => _actor.Name.ShouldEqual(_actorName);
        private It Should_set_actors_prefix_correctly = () => _actor.Prefix.ShouldEqual(_actorPrefix);
        private It Should_set_actor_id_correctly = () => _actor.ID.ShouldEqual(_actorId);
        

        private static string _actorName;
        private static string _actorPrefix;
        private static int _actorId;
        private static Actor _actor;
    }

    [Subject(typeof (Package))]
    public class When_adding_an_actor_with_invalid_id : PackageTestBase
    {
        private Because Of = () =>
                                 {
                                     _actor = new Actor("", "", -1);
                                 };

        private It Should_throw_invalid_operation_exception = () => Catch.Exception(() => Package.AddActor(_actor)).ShouldBeOfType<InvalidOperationException>();
        
        private static Actor _actor;
    }

    [Subject(typeof (Package))]
    public class When_adding_a_null_actor : PackageTestBase
    {
        private It Should_throw_null_reference_exception = () => Catch.Exception(() => Package.AddActor(null)).ShouldBeOfType<NullReferenceException>();
    }

    [Subject(typeof (Package))]
    public class When_adding_a_valid_actor : PackageTestBase
    {
        private Because Of = () =>
                                 {
                                     _actor = new Actor("", "", 1);
                                     Package.AddActor(_actor);
                                 };

        private It Should_contain_the_actor_in_its_collection = () => Package.Actors.ShouldContain(_actor);

        private It Should_set_the_actor_owner_to_the_package =
            () => Package.Actors[0].Owner.ShouldEqual(Package);
        
        
        private static Actor _actor;
    }

    [Subject(typeof (Package))]
    public class When_enumerating_actor_names : PackageTestBase
    {
        private Because Of = () =>
                                 {
                                     _actorName = A.Random.String.Resembling.A.FirstName;
                                     _actor = new Actor(_actorName, "", 1);
                                     Package.AddActor(_actor);
                                 };

        private It Should_return_the_actors_name_in_the_list = () => Package.GetActorNames().ShouldContain(_actorName);
        

        private static string _actorName;
        private static Actor _actor;
    }

    [Subject(typeof (Package))]
    public class When_enumerating_actor_names_with_embedded_package : PackageTestBase
    {
        private Because Of = () =>
                                 {
                                     var innerPackage = new Package();
                                     _innerActorName = A.Random.String.Resembling.A.FirstName;
                                     innerPackage.AddActor(new Actor(_innerActorName, "", 1));
                                     _actorName = A.Random.String.Resembling.A.LastName;  // Bug in UMMO..  FirstName is going to return the same value..  I think it's fixed in one of the feature branches
                                     Package.AddActor(new Actor(_actorName, "", 2));
                                     Package.AddPackage(innerPackage);

                                     _result = Package.GetActorNames();
                                 };

        private It Should_return_both_names = () => _result.Length.ShouldEqual(2);

        private It Should_return_inner_actors_name = () => _result.ShouldContain(_innerActorName);

        private It Should_return_outer_actors_name = () => _result.ShouldContain(_actorName);

        private static string _innerActorName;
        private static string _actorName;
        private static string[] _result;
    }

    [Subject(typeof (Package))]
    public class When_removing_an_actor : PackageTestBase
    {
        private Because Of = () =>
                                 {
                                     _actor = new Actor("", "", 1);
                                     Package.AddActor(_actor);
                                     Package.RemoveActor(_actor, "", "", "", "", false);
                                 };

        private It Should_remove_the_actor = () => Package.Actors.ShouldNotContain(_actor);
        
        private static Actor _actor;
    }

    [Subject(typeof (Package))]
    public class When_removing_an_actor_that_is_in_use_cases_and_sub_packages : PackageTestBase
    {
        private Because Of = () =>
                                 {
                                     _actor = new Actor("ActorName", "", 1);
                                     Package.AddActor(_actor);
                                     var useCase = new UseCase();
                                     useCase.AddActiveActor(_actor);
                                     Package.AddUseCase(useCase);
                                     var package = new Package();
                                     package.AddActor(_actor);
                                     Package.AddPackage(package);

                                     Package.RemoveActor(_actor, "", "", "", "", false);
                                 };

        private It Should_remove_the_actor_from_the_usecase =
            () => Package.UseCases[0].ActiveActors.ShouldNotContain(_actor);

        private It Should_not_remove_the_actor_from_the_sub_package = // Really???
            () => Package.Packages[0].Actors.ShouldContain(_actor); 

        private It Should_remove_the_actor = () => Package.Actors.ShouldNotContain(_actor);
        

        private static Actor _actor;
    }
}