using System.Xml.Serialization;

namespace UseCaseMakerLibrary.Contracts
{
    /// <summary>
    /// Interface for packages
    /// </summary>
    public interface IPackage : IIdentificableObject
    {
        /// <summary>
        /// Gets the actors.
        /// </summary>
        [XmlArray]
        [XmlArrayItem("Actor")]
        Actors Actors { get; }

        /// <summary>
        /// Gets or sets the packages.
        /// </summary>
        /// <value>
        /// The packages.
        /// </value>
        [XmlArray]
        [XmlArrayItem("Package")]
        Packages Packages { get; set; }

        /// <summary>
        /// Gets or sets the use cases.
        /// </summary>
        /// <value>
        /// The use cases.
        /// </value>
        [XmlArray]
        [XmlArrayItem("UseCase")]
        UseCases UseCases { get; set; }

        /// <summary>
        /// Gets or sets the requirements.
        /// </summary>
        /// <value>
        /// The requirements.
        /// </value>
        [XmlArray]
        [XmlArrayItem("Requirement")]
        Requirements Requirements { get; set; }

        /// <summary>
        /// Gets or sets the attributes.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        CommonAttributes Attributes { get; set; }

        /// <summary>
        /// Gets the object user view status.
        /// </summary>
        [XmlIgnore]
        UserViewStatus ObjectUserViewStatus { get; }

        /// <summary>
        /// Create a new actor
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="id">The id.</param>
        /// <returns>The created actor</returns>
        Actor NewActor(string name, string prefix, int id);

        /// <summary>
        /// Adds an actor to the package.
        /// </summary>
        /// <param name="actor">The actor.</param>
        void AddActor(Actor actor);

        /// <summary>
        /// Removes an actor from the package.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <param name="oldNameStartTag">The old name start tag.</param>
        /// <param name="oldNameEndTag">The old name end tag.</param>
        /// <param name="newNameStartTag">The new name start tag.</param>
        /// <param name="newNameEndTag">The new name end tag.</param>
        /// <param name="doNotMarkOccurrences">if set to <c>true</c> do not mark occurrences.</param>
        void RemoveActor(
            Actor actor,
            string oldNameStartTag,
            string oldNameEndTag,
            string newNameStartTag,
            string newNameEndTag,
            bool doNotMarkOccurrences);

        /// <summary>
        /// Gets the actor names.
        /// </summary>
        /// <returns>Names of the actors</returns>
        string[] GetActorNames();

        /// <summary>
        /// Creates a new package.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="id">The id.</param>
        /// <returns>The created package</returns>
        Package NewPackage(string name, string prefix, int id);

        /// <summary>
        /// Adds a package.
        /// </summary>
        /// <param name="package">The package.</param>
        void AddPackage(Package package);

        /// <summary>
        /// Removes a package.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="oldNameStartTag">The old name start tag.</param>
        /// <param name="oldNameEndTag">The old name end tag.</param>
        /// <param name="newNameStartTag">The new name start tag.</param>
        /// <param name="newNameEndTag">The new name end tag.</param>
        /// <param name="doNotMarkOccurrences">if set to <c>true</c> do not mark occurrences.</param>
        void RemovePackage(
            Package package,
            string oldNameStartTag,
            string oldNameEndTag,
            string newNameStartTag,
            string newNameEndTag,
            bool doNotMarkOccurrences);

        /// <summary>
        /// Creates a new use case.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="id">The id.</param>
        /// <returns>The created use case.</returns>
        UseCase NewUseCase(string name, string prefix, int id);

        /// <summary>
        /// Adds a use case.
        /// </summary>
        /// <param name="useCase">The use case.</param>
        void AddUseCase(UseCase useCase);

        /// <summary>
        /// Removes a use case.
        /// </summary>
        /// <param name="useCase">The use case.</param>
        /// <param name="oldNameStartTag">The old name start tag.</param>
        /// <param name="oldNameEndTag">The old name end tag.</param>
        /// <param name="newNameStartTag">The new name start tag.</param>
        /// <param name="newNameEndTag">The new name end tag.</param>
        /// <param name="doNotMarkOccurrences">if set to <c>true</c> do not mark occurrences.</param>
        void RemoveUseCase(
            UseCase useCase,
            string oldNameStartTag,
            string oldNameEndTag,
            string newNameStartTag,
            string newNameEndTag,
            bool doNotMarkOccurrences);

        /// <summary>
        /// Adds the requrement.
        /// </summary>
        /// <returns>The index of the added requirement</returns>
        int AddRequrement();

        /// <summary>
        /// Removes the requirement.
        /// </summary>
        /// <param name="requirement">The requirement.</param>
        void RemoveRequirement(Requirement requirement);

        /// <summary>
        /// Finds the requirement by unique ID.
        /// </summary>
        /// <param name="uniqueId">The unique ID.</param>
        /// <returns>The requirement.</returns>
        Requirement FindRequirementByUniqueID(string uniqueId);

        /// <summary>
        /// Replaces the name of the element.
        /// </summary>
        /// <param name="oldName">The old name.</param>
        /// <param name="oldNameStartTag">The old name start tag.</param>
        /// <param name="oldNameEndTag">The old name end tag.</param>
        /// <param name="newName">The new name.</param>
        /// <param name="newNameStartTag">The new name start tag.</param>
        /// <param name="newNameEndTag">The new name end tag.</param>
        void ReplaceElementName(
            string oldName,
            string oldNameStartTag,
            string oldNameEndTag,
            string newName,
            string newNameStartTag,
            string newNameEndTag);

        /// <summary>
        /// Replaces the element path.
        /// </summary>
        /// <param name="oldPath">The old path.</param>
        /// <param name="oldPathStartTag">The old path start tag.</param>
        /// <param name="oldPathEndTag">The old path end tag.</param>
        /// <param name="newPath">The new path.</param>
        /// <param name="newPathStartTag">The new path start tag.</param>
        /// <param name="newPathEndTag">The new path end tag.</param>
        void ReplaceElementPath(
            string oldPath,
            string oldPathStartTag,
            string oldPathEndTag,
            string newPath,
            string newPathStartTag,
            string newPathEndTag);
    }
}