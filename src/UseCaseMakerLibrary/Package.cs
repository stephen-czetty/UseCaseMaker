using System;
using System.Collections.Specialized;
using System.Linq;
using System.Xml.Serialization;
using UseCaseMakerLibrary.Contracts;

namespace UseCaseMakerLibrary
{
    /// <summary>
    /// Represents a package
    /// </summary>
    public class Package : IdentificableObject, IPackage
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Package"/> class.
        /// </summary>
        internal Package() : this(string.Empty, string.Empty, -1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Package"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="id">The id.</param>
        /// <param name="owner">The owner.</param>
        internal Package(string name, string prefix, int id, Package owner = null)
            : base(name, prefix, id, owner)
        {
            Attributes = new CommonAttributes();
            Requirements = new Requirements();
            Actors = new Actors(this);
            Packages = new Packages(this);
            UseCases = new UseCases(this);
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the actors.
        /// </summary>
        [XmlArray]
        [XmlArrayItem("Actor")]
        public Actors Actors { get; private set; }

        /// <summary>
        /// Gets or sets the packages.
        /// </summary>
        /// <value>
        /// The packages.
        /// </value>
        [XmlArray]
        [XmlArrayItem("Package")]
        public Packages Packages { get;  set; }

        /// <summary>
        /// Gets or sets the use cases.
        /// </summary>
        /// <value>
        /// The use cases.
        /// </value>
        [XmlArray]
        [XmlArrayItem("UseCase")]
        public UseCases UseCases { get;  set; }

        /// <summary>
        /// Gets or sets the requirements.
        /// </summary>
        /// <value>
        /// The requirements.
        /// </value>
        [XmlArray]
        [XmlArrayItem("Requirement")]
        public Requirements Requirements { get;  set; }

        /// <summary>
        /// Gets or sets the attributes.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        public CommonAttributes Attributes { get;  set; }

        #endregion

        #region Public Methods
        #region Actors Handling
        /// <summary>
        /// Create a new actor
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="id">The id.</param>
        /// <returns>
        /// The created actor
        /// </returns>
        public Actor NewActor(string name, string prefix, int id)
        {
            var actor = new Actor(name, prefix, id, this);
            return actor;
        }

        /// <summary>
        /// Adds an actor to the package.
        /// </summary>
        /// <param name="actor">The actor.</param>
        public void AddActor(Actor actor)
        {
            ValidateActor(actor);
            actor.Owner = this;
            Actors.Add(actor);
        }

        /// <summary>
        /// Removes an actor from the package.
        /// </summary>
        /// <param name="actor">The actor.</param>
        /// <param name="oldNameStartTag">The old name start tag.</param>
        /// <param name="oldNameEndTag">The old name end tag.</param>
        /// <param name="newNameStartTag">The new name start tag.</param>
        /// <param name="newNameEndTag">The new name end tag.</param>
        /// <param name="doNotMarkOccurrences">if set to <c>true</c> do not mark occurrences.</param>
        public void RemoveActor(
            Actor actor,
            string oldNameStartTag,
            string oldNameEndTag,
            string newNameStartTag,
            string newNameEndTag,
            bool doNotMarkOccurrences)
        {
            ValidateActor(actor);
            this.PurgeReferences(
                actor,
                null,
                oldNameStartTag,
                oldNameEndTag,
                newNameStartTag,
                newNameEndTag,
                doNotMarkOccurrences);
            Actors.Remove(actor);
        }

        /// <summary>
        /// Gets the actor names.
        /// </summary>
        /// <returns>
        /// Names of the actors
        /// </returns>
        public string[] GetActorNames()
        {
            var sc = new StringCollection();

            RecursiveGetActorNameList(sc);

            var actorNames = new string[sc.Count];
            sc.CopyTo(actorNames, 0);

            return actorNames;
        }
        #endregion // Actors Handling

        #region Packages Handling
        /// <summary>
        /// Creates a new package.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="id">The id.</param>
        /// <returns>
        /// The created package
        /// </returns>
        public Package NewPackage(string name, string prefix, int id)
        {
            var package = new Package(name, prefix, id, this);
            return package;
        }

        /// <summary>
        /// Adds a package.
        /// </summary>
        /// <param name="package">The package.</param>
        public void AddPackage(Package package)
        {
            package.Owner = this;
            Packages.Add(package);
        }

        /// <summary>
        /// Removes a package.
        /// </summary>
        /// <param name="package">The package.</param>
        /// <param name="oldNameStartTag">The old name start tag.</param>
        /// <param name="oldNameEndTag">The old name end tag.</param>
        /// <param name="newNameStartTag">The new name start tag.</param>
        /// <param name="newNameEndTag">The new name end tag.</param>
        /// <param name="doNotMarkOccurrences">if set to <c>true</c> do not mark occurrences.</param>
        public void RemovePackage(
            Package package,
            string oldNameStartTag,
            string oldNameEndTag,
            string newNameStartTag,
            string newNameEndTag,
            bool doNotMarkOccurrences)
        {
            this.PurgeReferences(
                package,
                null,
                oldNameStartTag,
                oldNameEndTag,
                newNameStartTag,
                newNameEndTag,
                doNotMarkOccurrences);
            Packages.Remove(package);
        }

        #endregion // Packages Handling

        #region UseCases Handling
        /// <summary>
        /// Creates a new use case.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="id">The id.</param>
        /// <returns>
        /// The created use case.
        /// </returns>
        public UseCase NewUseCase(string name, string prefix, int id)
        {
            var useCase = new UseCase(name, prefix, id, this);
            return useCase;
        }

        /// <summary>
        /// Adds a use case.
        /// </summary>
        /// <param name="useCase">The use case.</param>
        public void AddUseCase(UseCase useCase)
        {
            useCase.Owner = this;
            UseCases.Add(useCase);
        }

        /// <summary>
        /// Removes a use case.
        /// </summary>
        /// <param name="useCase">The use case.</param>
        /// <param name="oldNameStartTag">The old name start tag.</param>
        /// <param name="oldNameEndTag">The old name end tag.</param>
        /// <param name="newNameStartTag">The new name start tag.</param>
        /// <param name="newNameEndTag">The new name end tag.</param>
        /// <param name="doNotMarkOccurrences">if set to <c>true</c> do not mark occurrences.</param>
        public void RemoveUseCase(
            UseCase useCase,
            string oldNameStartTag,
            string oldNameEndTag,
            string newNameStartTag,
            string newNameEndTag,
            bool doNotMarkOccurrences)
        {
            this.PurgeReferences(
                useCase,
                null,
                oldNameStartTag,
                oldNameEndTag,
                newNameStartTag,
                newNameEndTag,
                doNotMarkOccurrences);
            UseCases.Remove(useCase);
        }

        #endregion // UseCase Handling

        #region Requirements Handling
        /// <summary>
        /// Adds the requirement.
        /// </summary>
        /// <returns>
        /// The index of the added requirement
        /// </returns>
        public int AddRequrement()
        {
            var requirement = new Requirement();
            int index = Requirements.Count;

            requirement.Id = index == 0 ? 1 : this.Requirements[index - 1].Id + 1;

            this.Requirements.Add(requirement);

            return Requirements.Count - 1;
        }

        /// <summary>
        /// Removes the requirement.
        /// </summary>
        /// <param name="requirement">The requirement.</param>
        public void RemoveRequirement(Requirement requirement)
        {
            foreach (
                Requirement tmpRequirement in
                    this.Requirements.Where(tmpRequirement => tmpRequirement.Id > requirement.Id))
                tmpRequirement.Id -= 1;

            this.Requirements.Remove(requirement);
        }

        /// <summary>
        /// Finds the requirement by unique ID.
        /// </summary>
        /// <param name="uniqueId">The unique ID.</param>
        /// <returns>The requirement.</returns>
        public Requirement FindRequirementByUniqueID(string uniqueId)
        {
            Requirement requirement = null;

            foreach (
                Requirement tmpRequirement in
                    this.Requirements.Where(tmpRequirement => tmpRequirement.UniqueId == uniqueId))
                requirement = tmpRequirement;

            return requirement;
        }
        #endregion

        #region Common Handling
        /// <summary>
        /// Replaces the name of the element.
        /// </summary>
        /// <param name="oldName">The old name.</param>
        /// <param name="oldNameStartTag">The old name start tag.</param>
        /// <param name="oldNameEndTag">The old name end tag.</param>
        /// <param name="newName">The new name.</param>
        /// <param name="newNameStartTag">The new name start tag.</param>
        /// <param name="newNameEndTag">The new name end tag.</param>
        public void ReplaceElementName(
            string oldName,
            string oldNameStartTag,
            string oldNameEndTag,
            string newName,
            string newNameStartTag,
            string newNameEndTag)
        {
            this.ChangeReferences(
                oldName,
                oldNameStartTag,
                oldNameEndTag,
                newName,
                newNameStartTag,
                newNameEndTag,
                true);
        }

        /// <summary>
        /// Replaces the element path.
        /// </summary>
        /// <param name="oldPath">The old path.</param>
        /// <param name="oldPathStartTag">The old path start tag.</param>
        /// <param name="oldPathEndTag">The old path end tag.</param>
        /// <param name="newPath">The new path.</param>
        /// <param name="newPathStartTag">The new path start tag.</param>
        /// <param name="newPathEndTag">The new path end tag.</param>
        public void ReplaceElementPath(
            string oldPath,
            string oldPathStartTag,
            string oldPathEndTag,
            string newPath,
            string newPathStartTag,
            string newPathEndTag)
        {
            this.ChangeReferences(
                oldPath,
                oldPathStartTag,
                oldPathEndTag,
                newPath,
                newPathStartTag,
                newPathEndTag,
                true);
        }
        #endregion

        /// <summary>
        /// Purges the references.
        /// </summary>
        /// <param name="thisPackage">The this package.</param>
        /// <param name="currentPackage">The current package.</param>
        /// <param name="oldNameStartTag">The old name start tag.</param>
        /// <param name="oldNameEndTag">The old name end tag.</param>
        /// <param name="newNameStartTag">The new name start tag.</param>
        /// <param name="newNameEndTag">The new name end tag.</param>
        /// <param name="doNotMark">if set to <c>true</c> do not mark.</param>
        public override void PurgeReferences(Package thisPackage, Package currentPackage, string oldNameStartTag, string oldNameEndTag, string newNameStartTag, string newNameEndTag, bool doNotMark)
        {
            foreach (Actor a in this.Actors)
            {
                currentPackage.PurgeReferences(
                    a,
                    null,
                    oldNameStartTag,
                    oldNameEndTag,
                    newNameStartTag,
                    newNameEndTag,
                    doNotMark);
            }

            foreach (UseCase uc in this.UseCases)
            {
                currentPackage.PurgeReferences(
                    uc,
                    null,
                    oldNameStartTag,
                    oldNameEndTag,
                    newNameStartTag,
                    newNameEndTag,
                    doNotMark);
            }

            foreach (Package p in this.Packages)
            {
                currentPackage.PurgeReferences(
                    p,
                    null,
                    oldNameStartTag,
                    oldNameEndTag,
                    newNameStartTag,
                    newNameEndTag,
                    doNotMark);
            }

            if (!doNotMark)
            {
                thisPackage.ChangeReferences(
                    this.Name,
                    oldNameStartTag,
                    oldNameEndTag,
                    this.Name,
                    newNameStartTag,
                    newNameEndTag,
                    false);
                thisPackage.ChangeReferences(
                    this.Path,
                    oldNameStartTag,
                    oldNameEndTag,
                    this.Path,
                    newNameStartTag,
                    newNameEndTag,
                    false);
            }
        }

        #endregion

        #region Internal Methods
        /// <summary>
        /// Changes the references.
        /// </summary>
        /// <param name="oldName">The old name.</param>
        /// <param name="oldNameStartTag">The old name start tag.</param>
        /// <param name="oldNameEndTag">The old name end tag.</param>
        /// <param name="newName">The new name.</param>
        /// <param name="newNameStartTag">The new name start tag.</param>
        /// <param name="newNameEndTag">The new name end tag.</param>
        /// <param name="deep">if set to <c>true</c> [deep].</param>
        internal void ChangeReferences(
            string oldName,
            string oldNameStartTag,
            string oldNameEndTag,
            string newName,
            string newNameStartTag,
            string newNameEndTag,
            bool deep)
        {
            string oldFullName = oldNameStartTag + oldName + oldNameEndTag;
            string newFullName = newNameStartTag + newName + newNameEndTag;

            foreach (Requirement requirement in this.Requirements)
                requirement.Description = requirement.Description.Replace(oldFullName, newFullName);

            foreach (UseCase useCase in this.UseCases)
            {
                foreach (OpenIssue openIssue in useCase.OpenIssues)
                    openIssue.Description = openIssue.Description.Replace(oldFullName, newFullName);

                foreach (Step step in useCase.Steps)
                    step.Description = step.Description.Replace(oldFullName, newFullName);

                if (useCase.Prose != null)
                    useCase.Prose = useCase.Prose.Replace(oldFullName, newFullName);
            }

            if (!deep)
                return;

            foreach (Package package in this.Packages)
            {
                package.ChangeReferences(
                    oldName,
                    oldNameStartTag,
                    oldNameEndTag,
                    newName,
                    newNameStartTag,
                    oldNameEndTag,
                    true);
            }
        }

        /// <summary>
        /// Purges the references.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="targetPackage">The target package.</param>
        /// <param name="oldNameStartTag">The old name start tag.</param>
        /// <param name="oldNameEndTag">The old name end tag.</param>
        /// <param name="newNameStartTag">The new name start tag.</param>
        /// <param name="newNameEndTag">The new name end tag.</param>
        /// <param name="doNotMark">if set to <c>true</c> [do not mark].</param>
        internal void PurgeReferences(
            IIdentificableObject element,
            Package targetPackage,
            string oldNameStartTag,
            string oldNameEndTag,
            string newNameStartTag,
            string newNameEndTag,
            bool doNotMark)
        {
            Package currentPackage = this;

            if (targetPackage == null)
            {
                while (currentPackage.Owner != null)
                {
                    currentPackage = currentPackage.Owner;
                }
            }

            element.PurgeReferences(this, currentPackage, oldNameStartTag, oldNameEndTag, newNameStartTag, newNameEndTag, doNotMark);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Validates the actor.
        /// </summary>
        /// <param name="actor">The actor.</param>
// ReSharper disable UnusedParameter.Local
        private static void ValidateActor(Actor actor)
// ReSharper restore UnusedParameter.Local
        {
            if (actor == null)
                throw new NullReferenceException("Actor cannot be null");

            if (actor.Id == -1)
                throw new InvalidOperationException("Actor must have a valid ID");
        }

        /// <summary>
        /// Get actor name list recursively.
        /// </summary>
        /// <param name="sc">The <see cref="StringCollection">StringCollection</see> containing all actor's names.</param>
        private void RecursiveGetActorNameList(StringCollection sc)
        {
            foreach (Actor actor in Actors)
                sc.Add(actor.Name);

            foreach (Package subPackage in Packages)
                subPackage.RecursiveGetActorNameList(sc);
        }
        #endregion
    }
}
