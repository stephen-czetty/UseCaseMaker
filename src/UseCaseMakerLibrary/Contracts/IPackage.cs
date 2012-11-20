using System;
using System.Xml.Serialization;

namespace UseCaseMakerLibrary.Contracts
{
    public interface IPackage : IIdentificableObject
    {
        Actor NewActor(String name, String prefix, Int32 id);

        void AddActor(Actor actor);

        void RemoveActor(
            Actor actor,
            String oldNameStartTag,
            String oldNameEndTag,
            String newNameStartTag,
            String newNameEndTag,
            Boolean dontMarkOccurrences);

        String [] GetActorNames();

        Package NewPackage(String name, String prefix, Int32 id);

        void AddPackage(Package package);

        void RemovePackage(
            Package package,
            String oldNameStartTag,
            String oldNameEndTag,
            String newNameStartTag,
            String newNameEndTag,
            Boolean dontMarkOccurrences);

        UseCase NewUseCase(String name, String prefix, Int32 id);

        void AddUseCase(UseCase useCase);

        void RemoveUseCase(
            UseCase useCase,
            String oldNameStartTag,
            String oldNameEndTag,
            String newNameStartTag,
            String newNameEndTag,
            Boolean dontMarkOccurrences);

        Int32 AddRequrement();

        void RemoveRequirement(Requirement requirement);

        Requirement FindRequirementByUniqueID(String uniqueID);

        void ReplaceElementName(
            String oldName,
            String oldNameStartTag,
            String oldNameEndTag,
            String newName,
            String newNameStartTag,
            String newNameEndTag);

        void ReplaceElementPath(
            String oldPath,
            String oldPathStartTag,
            String oldPathEndTag,
            String newPath,
            String newPathStartTag,
            String newPathEndTag);

        [XmlArray]
        [XmlArrayItem("Actor")]
        Actors Actors { get; }

        [XmlArray]
        [XmlArrayItem("Package")]
        Packages Packages { get; set; }

        [XmlArray]
        [XmlArrayItem("UseCase")]
        UseCases UseCases { get; set; }

        [XmlArray]
        [XmlArrayItem("Requirement")]
        Requirements Requirements { get; set; }

        CommonAttributes Attributes { get; set; }

        [XmlIgnore]
        UserViewStatus ObjectUserViewStatus { get; }
    }
}