using System;
using System.Xml.Serialization;

namespace UseCaseMakerLibrary
{
    /// <summary>
    /// A use case with steps
    /// </summary>
    public class UseCase : IdentificableObject
    {
        #region Class Members

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="UseCase"/> class.
        /// </summary>
        internal UseCase()
            : this(string.Empty, string.Empty, -1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UseCase"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="id">The id.</param>
        /// <param name="owner">The owner.</param>
        internal UseCase(string name, string prefix, int id, Package owner = null)
            : base(name, prefix, id, owner)
        {
            Status = StatusValue.Named;
            Level = LevelValue.Summary;
            Implementation = ImplementationValue.Scheduled;
            Complexity = ComplexityValue.Low;
            Priority = 1;
            AssignedTo = string.Empty;
            Release = string.Empty;
            Postconditions = string.Empty;
            Preconditions = string.Empty;
            Prose = string.Empty;
            ActiveActors = new ActiveActors();
            HistoryItems = new HistoryItems();
            OpenIssues = new OpenIssues();
            Steps = new Steps();
            Attributes = new CommonAttributes();
        }

        #endregion

        #region Public Constants and Enumerators
        /// <summary>
        /// Level of complexity
        /// </summary>
        public enum ComplexityValue
        {
            /// <summary>
            /// Low complexity
            /// </summary>
            Low = 0,

            /// <summary>
            /// Medium complexity
            /// </summary>
            Medium = 1,

            /// <summary>
            /// High complexity
            /// </summary>
            High = 2
        }

        /// <summary>
        /// Level of implementation
        /// </summary>
        public enum ImplementationValue
        {
            /// <summary>
            /// The case has been Scheduled
            /// </summary>
            Scheduled = 0,

            /// <summary>
            /// The case has been Started
            /// </summary>
            Started = 1,

            /// <summary>
            /// The case is Partially completed
            /// </summary>
            Partial = 2,

            /// <summary>
            /// The case is Completed
            /// </summary>
            Completed = 3,

            /// <summary>
            /// The case has been Deferred
            /// </summary>
            Deferred = 4,
        }

        /// <summary>
        /// Type of use case
        /// </summary>
        public enum LevelValue
        {
            /// <summary>
            /// Summary Level
            /// </summary>
            Summary = 0,

            /// <summary>
            /// User Level
            /// </summary>
            User = 1,

            /// <summary>
            /// Sub function level
            /// </summary>
            Subfunction = 2
        }

        /// <summary>
        /// Status of use case
        /// </summary>
        public enum StatusValue
        {
            /// <summary>
            /// Named status
            /// </summary>
            Named = 0,

            /// <summary>
            /// Initial status
            /// </summary>
            Initial = 1,

            /// <summary>
            /// Base status
            /// </summary>
            Base = 2,

            /// <summary>
            /// Completed status
            /// </summary>
            Completed = 3,

            /// <summary>
            /// Deferred status
            /// </summary>
            Deferred = 4,

            /// <summary>
            /// Tested status
            /// </summary>
            Tested = 5,

            /// <summary>
            /// Approved status
            /// </summary>
            Approved = 6
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the attributes.
        /// </summary>
        /// <value>
        /// The attributes.
        /// </value>
        public CommonAttributes Attributes { get; set; }

        /// <summary>
        /// Gets or sets the steps.
        /// </summary>
        /// <value>
        /// The steps.
        /// </value>
        [XmlArray]
        [XmlArrayItem("Step")]
        public Steps Steps { get; set; }

        /// <summary>
        /// Gets or sets the open issues.
        /// </summary>
        /// <value>
        /// The open issues.
        /// </value>
        [XmlArray]
        [XmlArrayItem("OpenIssue")]
        public OpenIssues OpenIssues { get; set; }

        /// <summary>
        /// Gets or sets the active actors.
        /// </summary>
        /// <value>
        /// The active actors.
        /// </value>
        [XmlArray]
        [XmlArrayItem("ActiveActor")]
        public ActiveActors ActiveActors { get; set; }

        /// <summary>
        /// Gets or sets the history items.
        /// </summary>
        /// <value>
        /// The history items.
        /// </value>
        [XmlArray]
        [XmlArrayItem("HistoryItem")]
        public HistoryItems HistoryItems { get; set; }

        /// <summary>
        /// Gets or sets the prose.
        /// </summary>
        /// <value>
        /// The prose.
        /// </value>
        public string Prose { get; set; }

        /// <summary>
        /// Gets or sets the preconditions.
        /// </summary>
        /// <value>
        /// The preconditions.
        /// </value>
        public string Preconditions { get; set; }

        /// <summary>
        /// Gets or sets the post conditions.
        /// </summary>
        /// <value>
        /// The post conditions.
        /// </value>
        public string Postconditions { get; set; }

        /// <summary>
        /// Gets or sets the release.
        /// </summary>
        /// <value>
        /// The release.
        /// </value>
        public string Release { get; set; }

        /// <summary>
        /// Gets or sets the assigned to.
        /// </summary>
        /// <value>
        /// The assigned to.
        /// </value>
        public string AssignedTo { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>
        /// The priority.
        /// </value>
        public int Priority { get; set; }

        /// <summary>
        /// Gets or sets the complexity.
        /// </summary>
        /// <value>
        /// The complexity.
        /// </value>
        public ComplexityValue Complexity { get; set; }

        /// <summary>
        /// Gets or sets the implementation.
        /// </summary>
        /// <value>
        /// The implementation.
        /// </value>
        public ImplementationValue Implementation { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public LevelValue Level { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public StatusValue Status { get; set; }

        #endregion

        #region Public Methods
        #region Step Handling
        /// <summary>
        /// Adds the step.
        /// </summary>
        /// <param name="previousStep">The previous step.</param>
        /// <param name="type">The type.</param>
        /// <param name="stereotype">The stereotype.</param>
        /// <param name="referencedUseCase">The referenced use case.</param>
        /// <param name="referenceType">Type of the reference.</param>
        /// <returns>The index of the new step</returns>
        public int AddStep(
            Step previousStep,
            Step.StepType type,
            string stereotype,
            UseCase referencedUseCase,
            DependencyItem.ReferenceType referenceType)
        {
            var step = new Step();
            int index;

            if (referenceType != DependencyItem.ReferenceType.None)
            {
                step.Dependency.Stereotype = stereotype;
                step.Dependency.PartnerUniqueID = referencedUseCase.UniqueId;
                step.Dependency.Type = referenceType;
                step.Description = (step.Dependency.Stereotype != "") ? "<<" + step.Dependency.Stereotype + ">>" : "";
                step.Description += " \"";
                step.Description += referencedUseCase.Name;
                step.Description += "\"";
            }

            if (previousStep == null)
            {
                step.Id = 1;
                Steps.Add(step);
                return Steps.Count - 1;
            }

            switch (type)
            {
                case Step.StepType.Default:
                    if (previousStep.Type == Step.StepType.Default)
                    {
                        step.Id = previousStep.Id;
                        index = FindStepIndexByUniqueId(previousStep.UniqueId) + 1;
                        while (true)
                        {
                            if (index == Steps.Count)
                            {
                                previousStep = Steps[index - 1];
                                break;
                            }

                            Step tmpStep = Steps[index];
                            if (tmpStep.Id != step.Id)
                            {
                                previousStep = Steps[index - 1];
                                break;
                            }

                            index += 1;
                        }

                        step.Id = previousStep.Id + 1;
                        foreach (Step tmpStep in Steps)
                        {
                            if (tmpStep.Id >= step.Id)
                            {
                                tmpStep.Id += 1;
                            }
                        }
                    }
                    else if (previousStep.Type == Step.StepType.Alternative)
                    {
                        step.Id = previousStep.Id;
                        step.Type = Step.StepType.Alternative;

                        index = FindStepIndexByUniqueId(previousStep.UniqueId) + 1;
                        while (true)
                        {
                            if (index == Steps.Count)
                            {
                                previousStep = Steps[index - 1];
                                break;
                            }

                            Step tmpStep = Steps[index];
                            if (tmpStep.Id != step.Id || tmpStep.Prefix == string.Empty)
                            {
                                previousStep = Steps[index - 1];
                                break;
                            }

                            index += 1;
                        }

                        step.Prefix = previousStep.Prefix;
                        if (step.Prefix != string.Empty)
                        {
                            char nextChar = step.Prefix[0];
                            nextChar++;
                            step.Prefix = new string(nextChar, 1);
                        }
                        else
                            step.Prefix = "A";

                        foreach (Step tmpStep in Steps)
                        {
                            if (tmpStep.Id == step.Id)
                            {
                                if (tmpStep.Prefix != string.Empty && tmpStep.Prefix.CompareTo(step.Prefix) >= 0)
                                {
                                    char nextChar = tmpStep.Prefix[0];
                                    nextChar++;
                                    tmpStep.Prefix = new string(nextChar, 1);
                                }
                            }
                        }
                    }
                    else if (previousStep.Type == Step.StepType.AlternativeChild)
                    {
                        step.Type = Step.StepType.AlternativeChild;
                        step.Id = previousStep.Id;
                        step.Prefix = previousStep.Prefix;

                        index = FindStepIndexByUniqueId(previousStep.UniqueId) + 1;
                        while (true)
                        {
                            if (index == Steps.Count)
                            {
                                previousStep = Steps[index - 1];
                                break;
                            }

                            Step tmpStep = Steps[index];
                            if (tmpStep.Id != step.Id || tmpStep.Prefix != step.Prefix)
                            {
                                previousStep = Steps[index - 1];
                                break;
                            }

                            index += 1;
                        }

                        step.Prefix = previousStep.Prefix;
                        step.ChildID = previousStep.ChildID + 1;
                    }

                    break;
                case Step.StepType.Alternative:
                    if (previousStep.Type == Step.StepType.Default)
                    {
                        step.Id = previousStep.Id;
                        step.Type = Step.StepType.Alternative;

                        index = this.FindStepIndexByUniqueId(previousStep.UniqueId) + 1;
                        while (true)
                        {
                            if (index == Steps.Count)
                            {
                                previousStep = Steps[index - 1];
                                break;
                            }

                            Step tmpStep = Steps[index];
                            if (tmpStep.Id != step.Id || tmpStep.Prefix == string.Empty)
                            {
                                previousStep = Steps[index - 1];
                                break;
                            }

                            index += 1;
                        }

                        step.Prefix = previousStep.Prefix;
                        if (step.Prefix != string.Empty)
                        {
                            char nextChar = step.Prefix[0];
                            nextChar++;
                            step.Prefix = new string(nextChar, 1);
                        }
                        else
                            step.Prefix = "A";

                        foreach (Step tmpStep in Steps)
                        {
                            if (tmpStep.Id == step.Id)
                            {
                                if (tmpStep.Prefix != string.Empty && tmpStep.Prefix.CompareTo(step.Prefix) >= 0)
                                {
                                    char nextChar = tmpStep.Prefix[0];
                                    nextChar++;
                                    tmpStep.Prefix = new string(nextChar, 1);
                                }
                            }
                        }
                    }
                    else if (previousStep.Type == Step.StepType.Alternative)
                    {
                        step.Type = Step.StepType.AlternativeChild;
                        step.Id = previousStep.Id;
                        step.Prefix = previousStep.Prefix;
                        step.ChildID = 1;
                    }

                    break;
            }

            index = FindStepIndexByUniqueId(previousStep.UniqueId) + 1;
            if (index == Steps.Count)
            {
                Steps.Add(step);
                return Steps.Count - 1;
            }

            this.Steps.Insert(index, step);
            return index;
        }

        /// <summary>
        /// Inserts the step.
        /// </summary>
        /// <param name="previousStep">The previous step.</param>
        /// <param name="stereotype">The stereotype.</param>
        /// <param name="referencedUseCase">The referenced use case.</param>
        /// <param name="referenceType">Type of the reference.</param>
        /// <returns>The index of the inserted step</returns>
        public int InsertStep(
            Step previousStep,
            string stereotype,
            UseCase referencedUseCase,
            DependencyItem.ReferenceType referenceType)
        {
            var step = new Step();

            if (referenceType != DependencyItem.ReferenceType.None)
            {
                step.Dependency.Stereotype = stereotype;
                step.Dependency.PartnerUniqueID = referencedUseCase.UniqueId;
                step.Dependency.Type = referenceType;
                step.Description = (step.Dependency.Stereotype != "") ? "<<" + step.Dependency.Stereotype + ">>" : "";
                step.Description += " \"";
                step.Description += referencedUseCase.Name;
                step.Description += "\"";
            }

            if (previousStep.Type == Step.StepType.Default)
            {
                step.Id = previousStep.Id;
                foreach (Step tmpStep in this.Steps)
                    if (tmpStep.Id >= step.Id)
                        tmpStep.Id += 1;
            }
            else if (previousStep.Type == Step.StepType.Alternative)
            {
                step.Prefix = previousStep.Prefix;
                if (step.Prefix == string.Empty)
                    step.Prefix = "A";

                step.Id = previousStep.Id;
                step.Type = Step.StepType.Alternative;

                foreach (Step tmpStep in Steps)
                {
                    if (tmpStep.Id == step.Id)
                    {
                        if (tmpStep.Prefix != string.Empty && tmpStep.Prefix.CompareTo(step.Prefix) >= 0)
                        {
                            char nextChar = tmpStep.Prefix[0];
                            nextChar++;
                            tmpStep.Prefix = new string(nextChar, 1);
                        }
                    }
                }
            }
            else if (previousStep.Type == Step.StepType.AlternativeChild)
            {
                step.Type = Step.StepType.AlternativeChild;
                step.Id = previousStep.Id;
                step.Prefix = previousStep.Prefix;
                step.ChildID = previousStep.ChildID;
                foreach (Step tmpStep in Steps)
                    if (tmpStep.Id == step.Id && tmpStep.Prefix == step.Prefix)
                        if (tmpStep.ChildID >= step.ChildID)
                            tmpStep.ChildID += 1;
            }

            int index = FindStepIndexByUniqueId(previousStep.UniqueId);
            Steps.Insert(index, step);
            return index;
        }

        /// <summary>
        /// Removes the step.
        /// </summary>
        /// <param name="step">The step.</param>
        public void RemoveStep(Step step)
        {
            for (int i = Steps.Count - 1; i >= 0; i--)
            {
                Step tmpStep = Steps[i];
                switch (step.Type)
                {
                    case Step.StepType.Default:
                        if (tmpStep.Id == step.Id)
                            Steps.Remove(tmpStep);

                        if (tmpStep.Id > step.Id)
                            tmpStep.Id -= 1;

                        break;
                    case Step.StepType.Alternative:
                        if (tmpStep.Id == step.Id)
                        {
                            if (tmpStep.Prefix == step.Prefix)
                                Steps.Remove(tmpStep);

                            if (tmpStep.Prefix != string.Empty && tmpStep.Prefix.CompareTo(step.Prefix) > 0)
                            {
                                char nextChar = tmpStep.Prefix[0];
                                nextChar--;
                                tmpStep.Prefix = new string(nextChar, 1);
                            }
                        }

                        break;
                    case Step.StepType.AlternativeChild:
                        if (tmpStep.Id == step.Id && tmpStep.Prefix == step.Prefix)
                        {
                            if (tmpStep.ChildID == step.ChildID)
                                Steps.Remove(step);

                            if (tmpStep.ChildID > step.ChildID)
                                tmpStep.ChildID -= 1;
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Finds the step by unique ID.
        /// </summary>
        /// <param name="uniqueId">The unique ID.</param>
        /// <returns>The step or <code>null</code> if not found.</returns>
        public Step FindStepByUniqueID(string uniqueId)
        {
            Step step = null;

            foreach (Step tmpStep in Steps)
                if (tmpStep.UniqueId == uniqueId)
                    step = tmpStep;

            return step;
        }

        /// <summary>
        /// Steps the has alternatives.
        /// </summary>
        /// <param name="step">The step.</param>
        /// <returns><c>true</c> if there are alternative steps</returns>
        public bool StepHasAlternatives(Step step)
        {
            int index;
            Step tmpStep;

            if (step.Type == Step.StepType.AlternativeChild)
                return true;

            for (index = Steps.Count - 1; index >= 0; index--)
            {
                tmpStep = Steps[index];
                if (tmpStep.UniqueId == step.UniqueId)
                    break;
            }

            // Step is not found or it is the last step in collection
            if (index >= Steps.Count - 1)
                return false;

            tmpStep = Steps[index + 1];
            if (step.Type == Step.StepType.Default && tmpStep.Type == Step.StepType.Alternative)
                return true;

            return step.Type == Step.StepType.Alternative && tmpStep.Type == Step.StepType.AlternativeChild;
        }
        #endregion

        #region OpenIssues Handling
        /// <summary>
        /// Adds the open issue.
        /// </summary>
        /// <returns>The index of the new issue</returns>
        public int AddOpenIssue()
        {
            var openIssue = new OpenIssue();
            int index = OpenIssues.Count;

            openIssue.Id = index == 0 ? 1 : this.OpenIssues[index - 1].Id + 1;

            OpenIssues.Add(openIssue);

            return OpenIssues.Count - 1;
        }

        /// <summary>
        /// Removes the open issue.
        /// </summary>
        /// <param name="openIssue">The open issue.</param>
        public void RemoveOpenIssue(OpenIssue openIssue)
        {
            foreach (OpenIssue tmpOpenIssue in OpenIssues)
                if (tmpOpenIssue.Id > openIssue.Id)
                    tmpOpenIssue.Id -= 1;

            OpenIssues.Remove(openIssue);
        }

        /// <summary>
        /// Finds the open issue by unique ID.
        /// </summary>
        /// <param name="uniqueId">The unique ID.</param>
        /// <returns>The open issue, or <c>null</c> if not found</returns>
        public OpenIssue FindOpenIssueByUniqueID(string uniqueId)
        {
            OpenIssue openIssue = null;

            foreach (OpenIssue tmpOpenIssue in OpenIssues)
                if (tmpOpenIssue.UniqueId == uniqueId)
                    openIssue = tmpOpenIssue;

            return openIssue;
        }
        #endregion

        #region ActiveActors Handling
        /// <summary>
        /// Adds the active actor.
        /// </summary>
        /// <param name="actor">The actor.</param>
        public void AddActiveActor(Actor actor)
        {
            var aactor = new ActiveActor { ActorUniqueID = actor.UniqueId, IsPrimary = false };
            ActiveActors.Add(aactor);
        }

        /// <summary>
        /// Removes the active actor.
        /// </summary>
        /// <param name="actor">The actor.</param>
        public void RemoveActiveActor(Actor actor)
        {
            ActiveActor aactor = ActiveActors.FindByUniqueID(actor.UniqueId);
            if (aactor != null)
                ActiveActors.Remove(aactor);
        }
        #endregion

        #region History Item Handling
        /// <summary>
        /// Adds the history item.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="type">The type.</param>
        /// <param name="action">The action.</param>
        /// <param name="notes">The notes.</param>
        public void AddHistoryItem(DateTime date, HistoryItem.HistoryType type, int action, string notes)
        {
            var hi = new HistoryItem { Date = date, Type = type, Action = action, Notes = notes };
            HistoryItems.Add(hi);
        }

        /// <summary>
        /// Removes the history item.
        /// </summary>
        /// <param name="index">The index.</param>
        public void RemoveHistoryItem(int index)
        {
            HistoryItems.RemoveAt(index);
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
        /// <param name="doNotMark">if set to <c>true</c> [do not mark].</param>
        public override void PurgeReferences(Package thisPackage, Package currentPackage, string oldNameStartTag, string oldNameEndTag, string newNameStartTag, string newNameEndTag, bool doNotMark)
        {
            foreach (Package p in currentPackage.Packages)
                p.PurgeReferences(this, p, oldNameStartTag, oldNameEndTag, newNameStartTag, newNameEndTag, doNotMark);

            if (!doNotMark)
            {
                thisPackage.ChangeReferences(
                    this.Name, oldNameStartTag, oldNameEndTag, this.Name, newNameStartTag, newNameEndTag, false);

                thisPackage.ChangeReferences(
                    this.Path, oldNameStartTag, oldNameEndTag, this.Path, newNameStartTag, newNameEndTag, false);
            }

            // remove use case references in use case's steps
            foreach (UseCase uc in thisPackage.UseCases)
                foreach (Step step in uc.Steps)
                    if (step.Dependency.PartnerUniqueID == this.UniqueId)
                    {
                        step.Dependency.Type = DependencyItem.ReferenceType.None;
                        step.Dependency.PartnerUniqueID = "";
                        step.Dependency.Stereotype = "";
                    }
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Finds the step index by unique id.
        /// </summary>
        /// <param name="uniqueId">The unique ID.</param>
        /// <returns>The index of the step</returns>
        private int FindStepIndexByUniqueId(string uniqueId)
        {
            int ret = -1;

            for (int i = 0; i < this.Steps.Count; i++)
                if (Steps[i].UniqueId == uniqueId)
                    ret = i;

            return ret;
        }
        #endregion
    }
}
