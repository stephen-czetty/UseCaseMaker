using System;
using System.Collections.Specialized;
using System.Xml.Serialization;
using UseCaseMakerLibrary.Contracts;

namespace UseCaseMakerLibrary
{
    public class Package : IdentificableObject, IPackage
    {

        #region Constructors
        internal Package()
            : base()
        {
		    Attributes = new CommonAttributes();
		    Requirements = new Requirements();
		    this.Actors = new Actors(this);
			this.Packages = new Packages(this);
			this.UseCases = new UseCases(this);
        }

        internal Package(String name, String prefix, Int32 id)
            : base(name,prefix,id)
        {
		    Attributes = new CommonAttributes();
		    Requirements = new Requirements();
		    this.Actors = new Actors(this);
			this.Packages = new Packages(this);
			this.UseCases = new UseCases(this);
        }

        internal Package(String name, String prefix, Int32 id, Package owner)
            : base(name,prefix,id,owner)
        {
		    Attributes = new CommonAttributes();
		    Requirements = new Requirements();
		    this.Actors = new Actors(this);
			this.Packages = new Packages(this);
			this.UseCases = new UseCases(this);
        }
        #endregion

        #region Public Methods
        #region Actors Handling
        public Actor NewActor(String name, String prefix, Int32 id)
        {
            Actor actor = new Actor(name,prefix,id,this);
            return actor;
        }

        public void AddActor(Actor actor)
        {
            ValidateActor(actor);
            actor.Owner = this;
            Actors.Add(actor);
        }

        public void RemoveActor(
            Actor actor,
            String oldNameStartTag,
            String oldNameEndTag,
            String newNameStartTag,
            String newNameEndTag,
            Boolean dontMarkOccurrences)
        {
            ValidateActor(actor);
            this.PurgeReferences(
                actor,
                null,
                oldNameStartTag,
                oldNameEndTag,
                newNameStartTag,
                newNameEndTag,
                dontMarkOccurrences);
            Actors.Remove(actor);
        }

        public String [] GetActorNames()
        {
            StringCollection sc = new StringCollection();

            RecursiveGetActorNameList(sc);

            String [] actorNames = new String[sc.Count];
            sc.CopyTo(actorNames,0);

            return actorNames;
        }
        #endregion // Actors Handling

        #region Packages Handling
        public Package NewPackage(String name, String prefix, Int32 id)
        {
            Package package = new Package(name,prefix,id,this);
            return package;
        }

        public void AddPackage(Package package)
        {
            package.Owner = this;
            Packages.Add(package);
        }

        public void RemovePackage(
            Package package,
            String oldNameStartTag,
            String oldNameEndTag,
            String newNameStartTag,
            String newNameEndTag,
            Boolean dontMarkOccurrences)
        {
            this.PurgeReferences(
                package,
                null,
                oldNameStartTag,
                oldNameEndTag,
                newNameStartTag,
                newNameEndTag,
                dontMarkOccurrences);
            Packages.Remove(package);
        }

        #endregion // Packages Handling

        #region UseCases Handling
        public UseCase NewUseCase(String name, String prefix, Int32 id)
        {
            UseCase useCase = new UseCase(name,prefix,id,this);
            return useCase;
        }

        public void AddUseCase(UseCase useCase)
        {
            useCase.Owner = this;
            UseCases.Add(useCase);
        }

        public void RemoveUseCase(
            UseCase useCase,
            String oldNameStartTag,
            String oldNameEndTag,
            String newNameStartTag,
            String newNameEndTag,
            Boolean dontMarkOccurrences)
        {
            this.PurgeReferences(
                useCase,
                null,
                oldNameStartTag,
                oldNameEndTag,
                newNameStartTag,
                newNameEndTag,
                dontMarkOccurrences);
            UseCases.Remove(useCase);
        }

        #endregion // UseCase Handling

        #region Requirements Handling
        public Int32 AddRequrement()
        {
            Requirement requirement = new Requirement();
            Int32 index = Requirements.Count;
            Int32 ret;

            if(index == 0)
            {
                requirement.ID = 1;
            }
            else
            {
                requirement.ID = Requirements[index - 1].ID + 1;
            }

            this.Requirements.Add(requirement);

            return Requirements.Count - 1;
        }

        public void RemoveRequirement(Requirement requirement)
        {
            foreach(Requirement tmpRequirement in Requirements)
            {
                if(tmpRequirement.ID > requirement.ID)
                {
                    tmpRequirement.ID -= 1;
                }
            }
			this.Requirements.Remove(requirement);
        }

        public Requirement FindRequirementByUniqueID(String uniqueID)
        {
            Requirement requirement = null;

            foreach(Requirement tmpRequirement in Requirements)
            {
                if(tmpRequirement.UniqueID == uniqueID)
                {
                    requirement = tmpRequirement;
                }
            }

            return requirement;
        }
        #endregion

        #region Common Handling
        public void ReplaceElementName(
            String oldName,
            String oldNameStartTag,
            String oldNameEndTag,
            String newName,
            String newNameStartTag,
            String newNameEndTag)
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

        public void ReplaceElementPath(
            String oldPath,
            String oldPathStartTag,
            String oldPathEndTag,
            String newPath,
            String newPathStartTag,
            String newPathEndTag)
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
        #endregion

        #region Public Properties

        [XmlArray]
        [XmlArrayItem("Actor")]
	    public Actors Actors { get; private set; }

        [XmlArray]
        [XmlArrayItem("Package")]
        public Packages Packages { get;  set; }

        [XmlArray]
        [XmlArrayItem("UseCase")]
        public UseCases UseCases { get;  set; }

        [XmlArray]
        [XmlArrayItem("Requirement")]
        public Requirements Requirements { get;  set; }

        public CommonAttributes Attributes { get;  set; }

		#endregion

        #region Private Methods
        void ValidateActor(Actor actor)
        {
            if(actor == null)
            {
                throw new NullReferenceException("Actor cannot be null");
            }
            if(actor.ID == -1)
            {
                throw new InvalidOperationException("Actor must have a valid ID");
            }
        }

        void RecursiveGetActorNameList(StringCollection sc)
        {
            foreach(Actor actor in Actors)
            {
                sc.Add(actor.Name);
            }

            foreach(Package subPackage in Packages)
            {
                subPackage.RecursiveGetActorNameList(sc);
            }
        }
        #endregion

        #region Internal Methods
        internal void ChangeReferences(
            String oldName,
            String oldNameStartTag,
            String oldNameEndTag,
            String newName,
            String newNameStartTag,
            String newNameEndTag,
            Boolean deep)
        {
            String oldFullName = oldNameStartTag + oldName + oldNameEndTag;
            String newFullName = newNameStartTag + newName + newNameEndTag;

            foreach(Requirement requirement in Requirements)
            {
                requirement.Description = requirement.Description.Replace(oldFullName,newFullName);
            }

            foreach(UseCase useCase in UseCases)
            {
                foreach(OpenIssue openIssue in useCase.OpenIssues)
                {
                    openIssue.Description = openIssue.Description.Replace(oldFullName,newFullName);
                }
                foreach(Step step in useCase.Steps)
                {
                    step.Description = step.Description.Replace(oldFullName,newFullName);
                }
                if(useCase.Prose != null)
                {
                    useCase.Prose = useCase.Prose.Replace(oldFullName,newFullName);
                }
            }

            if(deep)
            {
                foreach(Package package in Packages)
                {
                    package.ChangeReferences(
                        oldName,
                        oldNameStartTag,
                        oldNameEndTag,
                        newName,
                        newNameStartTag,
                        oldNameEndTag,
                        deep);
                }
            }
        }

        public override void PurgeReferences(Package thisPackage, Package currentPackage, string oldNameStartTag, string oldNameEndTag, string newNameStartTag, string newNameEndTag, bool dontMark)
        {
            foreach(Actor a in Actors)
                {
                    currentPackage.PurgeReferences(
                        a,
                        null,
                        oldNameStartTag,
                        oldNameEndTag,
                        newNameStartTag,
                        newNameEndTag,
                        dontMark);
                }
                foreach(UseCase uc in UseCases)
                {
                    currentPackage.PurgeReferences(
                        uc,
                        null,
                        oldNameStartTag,
                        oldNameEndTag,
                        newNameStartTag,
                        newNameEndTag,
                        dontMark);
                }
                foreach(Package p in Packages)
                {
                    currentPackage.PurgeReferences(
                        p,
                        null,
                        oldNameStartTag,
                        oldNameEndTag,
                        newNameStartTag,
                        newNameEndTag,
                        dontMark);
                }
                if(!dontMark)
                {
                    thisPackage.ChangeReferences(
                        Name,
                        oldNameStartTag,
                        oldNameEndTag,
                        Name,
                        newNameStartTag,
                        newNameEndTag,
                        false);
                    thisPackage.ChangeReferences(
                        Path,
                        oldNameStartTag,
                        oldNameEndTag,
                        Path,
                        newNameStartTag,
                        newNameEndTag,
                        false);
                }
        }

        internal void PurgeReferences(
            IIdentificableObject element,
            Package targetPackage,
            String oldNameStartTag,
            String oldNameEndTag,
            String newNameStartTag,
            String newNameEndTag,
            Boolean dontMark)
        {
            Package currentPackage = this;

            if(targetPackage == null)
            {
                while(currentPackage.Owner != null)
                {
                    currentPackage = currentPackage.Owner;
                }
            }

            element.PurgeReferences(this, currentPackage, oldNameStartTag, oldNameEndTag, newNameStartTag, newNameEndTag, dontMark);
        }
        #endregion
    }
}
