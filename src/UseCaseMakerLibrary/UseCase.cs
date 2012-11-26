using System;
using System.Xml.Serialization;

namespace UseCaseMakerLibrary
{
    public class UseCase : IdentificableObject
    {
        #region Public Constants and Enumerators
        public enum ComplexityValue
        {
            Low = 0,
            Medium = 1,
            High = 2
        }

        public enum ImplementationValue
        {
            Scheduled = 0,
            Started = 1,
            Partial = 2,
            Completed = 3,
            Deferred = 4,
        }

        public enum LevelValue
        {
            Summary = 0,
            User = 1,
            Subfunction = 2
        }

        public enum StatusValue
        {
            Named = 0,
            Initial = 1,
            Base = 2,
            Completed = 3,
            Deferred = 4,
            Tested = 5,
            Approved = 6
        }
        #endregion

        #region Class Members

		#endregion

        #region Constructors
        internal UseCase()
            :this (string.Empty, string.Empty, -1)
        {
        }

        internal UseCase(String name, String prefix, Int32 id, Package owner = null)
            : base(name,prefix,id,owner)
		{
	        Status = StatusValue.Named;
	        Level = LevelValue.Summary;
	        Implementation = ImplementationValue.Scheduled;
	        Complexity = ComplexityValue.Low;
	        Priority = 1;
	        AssignedTo = String.Empty;
	        Release = String.Empty;
	        Postconditions = String.Empty;
	        Preconditions = String.Empty;
	        Prose = String.Empty;
	        ActiveActors = new ActiveActors();
	        HistoryItems = new HistoryItems();
	        OpenIssues = new OpenIssues();
	        Steps = new Steps();
	        Attributes = new CommonAttributes();
		}

		#endregion
    
        #region Public Properties

	    public CommonAttributes Attributes { get; set; }

        [XmlArray]
        [XmlArrayItem("Step")]
	    public Steps Steps { get; set; }

        [XmlArray]
        [XmlArrayItem("OpenIssue")]
	    public OpenIssues OpenIssues { get; set; }

        [XmlArray]
        [XmlArrayItem("ActiveActor")]
	    public ActiveActors ActiveActors { get; set; }

        [XmlArray]
        [XmlArrayItem("HistoryItem")]
	    public HistoryItems HistoryItems { get; set; }

	    public string Prose { get; set; }

	    public string Preconditions { get; set; }

	    public string Postconditions { get; set; }

	    public string Release { get; set; }

	    public string AssignedTo { get; set; }

	    public int Priority { get; set; }

	    public ComplexityValue Complexity { get; set; }

	    public ImplementationValue Implementation { get; set; }

	    public LevelValue Level { get; set; }

	    public StatusValue Status { get; set; }

	    #endregion

        #region Public Methods
        #region Step Handling
        public Int32 AddStep(
            Step previousStep,
            Step.StepType type,
            String stereotype,
            UseCase referencedUseCase,
            DependencyItem.ReferenceType referenceType)
        {
            Step step = new Step();
            Int32 index;
            Int32 ret;

            if(referenceType != DependencyItem.ReferenceType.None)
            {
                step.Dependency.Stereotype = stereotype;
                step.Dependency.PartnerUniqueID = referencedUseCase.UniqueId;
                step.Dependency.Type = referenceType;
                step.Description = (step.Dependency.Stereotype != "") ? "<<" + step.Dependency.Stereotype + ">>" : "";
                step.Description += " \"";
                step.Description += referencedUseCase.Name;
                step.Description += "\"";
            }

            if(previousStep == null)
            {
                step.Id = 1;
                Steps.Add(step);
                return Steps.Count - 1;
            }

            switch(type)
            {
                case Step.StepType.Default:
                    if(previousStep.Type == Step.StepType.Default)
                    {
                        step.Id = previousStep.Id;
                        index = FindStepIndexByUniqueId(previousStep.UniqueId) + 1;
                        while(true)
                        {
                            if(index == Steps.Count)
                            {
                                previousStep = Steps[index - 1];
                                break;
                            }
                            Step tmpStep = Steps[index];
                            if(tmpStep.Id != step.Id)
                            {
                                previousStep = Steps[index - 1];
                                break;
                            }
                            index += 1;
                        }
                        step.Id = previousStep.Id + 1;
                        foreach(Step tmpStep in Steps)
                        {
                            if(tmpStep.Id >= step.Id)
                            {
                                tmpStep.Id += 1;
                            }
                        }
                    }
                    else if(previousStep.Type == Step.StepType.Alternative)
                    {
                        step.Id = previousStep.Id;
                        step.Type = Step.StepType.Alternative;

                        index = FindStepIndexByUniqueId(previousStep.UniqueId) + 1;
                        while(true)
                        {
                            if(index == Steps.Count)
                            {
                                previousStep = Steps[index - 1];
                                break;
                            }
                            Step tmpStep = Steps[index];
                            if(tmpStep.Id != step.Id || tmpStep.Prefix == String.Empty)
                            {
                                previousStep = Steps[index - 1];
                                break;
                            }
                            index += 1;
                        }
                        step.Prefix = previousStep.Prefix;
                        if(step.Prefix != String.Empty)
                        {
                            Char nextChar = step.Prefix[0];
                            nextChar++;
                            step.Prefix = new String(nextChar,1);
                        }
                        else
                        {
                            step.Prefix = "A";
                        }
                        
                        foreach(Step tmpStep in Steps)
                        {
                            if(tmpStep.Id == step.Id)
                            {
                                if(tmpStep.Prefix != String.Empty && tmpStep.Prefix.CompareTo(step.Prefix) >= 0)
                                {
                                    Char nextChar = tmpStep.Prefix[0];
                                    nextChar++;
                                    tmpStep.Prefix = new String(nextChar,1);
                                }
                            }
                        }
                    }
                    else if(previousStep.Type == Step.StepType.AlternativeChild)
                    {
                        step.Type = Step.StepType.AlternativeChild;
                        step.Id = previousStep.Id;
                        step.Prefix = previousStep.Prefix;

                        index = FindStepIndexByUniqueId(previousStep.UniqueId) + 1;
                        while(true)
                        {
                            if(index == Steps.Count)
                            {
                                previousStep = Steps[index - 1];
                                break;
                            }
                            Step tmpStep = Steps[index];
                            if(tmpStep.Id != step.Id || tmpStep.Prefix != step.Prefix)
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
                    if(previousStep.Type == Step.StepType.Default)
                    {
                        step.Id = previousStep.Id;
                        step.Type = Step.StepType.Alternative;

                        index = this.FindStepIndexByUniqueId(previousStep.UniqueId) + 1;
                        while(true)
                        {
                            if(index == Steps.Count)
                            {
                                previousStep = Steps[index - 1];
                                break;
                            }
                            Step tmpStep = Steps[index];
                            if(tmpStep.Id != step.Id || tmpStep.Prefix == String.Empty)
                            {
                                previousStep = Steps[index - 1];
                                break;
                            }
                            index += 1;
                        }
                        step.Prefix = previousStep.Prefix;
                        if(step.Prefix != String.Empty)
                        {
                            Char nextChar = step.Prefix[0];
                            nextChar++;
                            step.Prefix = new String(nextChar,1);
                        }
                        else
                        {
                            step.Prefix = "A";
                        }
                        
                        foreach(Step tmpStep in Steps)
                        {
                            if(tmpStep.Id == step.Id)
                            {
                                if(tmpStep.Prefix != String.Empty && tmpStep.Prefix.CompareTo(step.Prefix) >= 0)
                                {
                                    Char nextChar = tmpStep.Prefix[0];
                                    nextChar++;
                                    tmpStep.Prefix = new String(nextChar,1);
                                }
                            }
                        }
                    }
                    else if(previousStep.Type == Step.StepType.Alternative)
                    {
                        step.Type = Step.StepType.AlternativeChild;
                        step.Id = previousStep.Id;
                        step.Prefix = previousStep.Prefix;
                        step.ChildID = 1;
                    }
                    break;
            }

            index = FindStepIndexByUniqueId(previousStep.UniqueId) + 1;
            if(index == Steps.Count)
            {
                Steps.Add(step);
                return Steps.Count - 1;
            }
            else
            {
                Steps.Insert(index, step);
                ret = index;
            }

            return ret;
        }

        public Int32 InsertStep(
            Step previousStep,
            String stereotype,
            UseCase referencedUseCase,
            DependencyItem.ReferenceType referenceType)
        {
            Step step = new Step();
            Int32 ret;

            if(referenceType != DependencyItem.ReferenceType.None)
            {
                step.Dependency.Stereotype = stereotype;
                step.Dependency.PartnerUniqueID = referencedUseCase.UniqueId;
                step.Dependency.Type = referenceType;
                step.Description = (step.Dependency.Stereotype != "") ? "<<" + step.Dependency.Stereotype + ">>" : "";
                step.Description += " \"";
                step.Description += referencedUseCase.Name;
                step.Description += "\"";
            }

            if(previousStep.Type == Step.StepType.Default)
            {
                step.Id = previousStep.Id;
				foreach(Step tmpStep in this.Steps)
                {
                    if(tmpStep.Id >= step.Id)
                    {
                        tmpStep.Id += 1;
                    }
                }
            }
            else if(previousStep.Type == Step.StepType.Alternative)
            {
                step.Prefix = previousStep.Prefix;
                if(step.Prefix == String.Empty)
                {
                    step.Prefix = "A";
                }
                step.Id = previousStep.Id;
                step.Type = Step.StepType.Alternative;

                foreach(Step tmpStep in Steps)
                {
                    if(tmpStep.Id == step.Id)
                    {
                        if(tmpStep.Prefix != String.Empty && tmpStep.Prefix.CompareTo(step.Prefix) >= 0)
                        {
                            Char nextChar = tmpStep.Prefix[0];
                            nextChar++;
                            tmpStep.Prefix = new String(nextChar,1);
                        }
                    }
                }
            }
            else if(previousStep.Type == Step.StepType.AlternativeChild)
            {
                step.Type = Step.StepType.AlternativeChild;
                step.Id = previousStep.Id;
                step.Prefix = previousStep.Prefix;
                step.ChildID = previousStep.ChildID;
                foreach(Step tmpStep in Steps)
                {
                    if(tmpStep.Id == step.Id && tmpStep.Prefix == step.Prefix)
                    {
                        if(tmpStep.ChildID >= step.ChildID)
                        {
                            tmpStep.ChildID += 1;
                        }
                    }
                }
            }

            int index = FindStepIndexByUniqueId(previousStep.UniqueId);
            Steps.Insert(index,step);
            ret = index;

            return ret;
        }

        public void RemoveStep(Step step)
        {
            for(int i = Steps.Count - 1; i >= 0; i--)
            {
                Step tmpStep = Steps[i];
                switch(step.Type)
                {
                    case Step.StepType.Default:
                        if(tmpStep.Id == step.Id)
                        {
                            Steps.Remove(tmpStep);
                        }
                        if(tmpStep.Id > step.Id)
                        {
                            tmpStep.Id -= 1;
                        }
                        break;
                    case Step.StepType.Alternative:
                        if(tmpStep.Id == step.Id)
                        {
                            if(tmpStep.Prefix == step.Prefix)
                            {
                                Steps.Remove(tmpStep);
                            }
                            if(tmpStep.Prefix != String.Empty && tmpStep.Prefix.CompareTo(step.Prefix) > 0)
                            {
                                Char nextChar = tmpStep.Prefix[0];
                                nextChar--;
                                tmpStep.Prefix = new String(nextChar,1);
                            }
                        }
                        break;
                    case Step.StepType.AlternativeChild:
                        if(tmpStep.Id == step.Id && tmpStep.Prefix == step.Prefix)
                        {
                            if(tmpStep.ChildID == step.ChildID)
                            {
                                Steps.Remove(step);
                            }
                            if(tmpStep.ChildID > step.ChildID)
                            {
                                tmpStep.ChildID -= 1;
                            }
                        }
                        break;
                }
            }
        }

        public Step FindStepByUniqueID(String uniqueID)
        {
            Step step = null;

            foreach(Step tmpStep in Steps)
            {
                if(tmpStep.UniqueId == uniqueID)
                {
                    step = tmpStep;
                }
            }

            return step;
        }

        public bool StepHasAlternatives(Step step)
        {
            Int32 index;
            Step tmpStep;

            if(step.Type == Step.StepType.AlternativeChild)
            {
                return true;
            }

            for(index = Steps.Count - 1; index >= 0; index--)
            {
                tmpStep = Steps[index];
                if(tmpStep.UniqueId == step.UniqueId)
                {
                    break;
                }
            }
            // Step is not found or it is the last step in collection
            if(index >= Steps.Count - 1)
            {
                return false;
            }
            tmpStep = Steps[index + 1];
            if(step.Type == Step.StepType.Default && tmpStep.Type == Step.StepType.Alternative)
            {
                return true;
            }
            if(step.Type == Step.StepType.Alternative && tmpStep.Type == Step.StepType.AlternativeChild)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region OpenIssues Handling
        public Int32 AddOpenIssue()
        {
            OpenIssue openIssue = new OpenIssue();
            Int32 index = OpenIssues.Count;

            if(index == 0)
            {
                openIssue.Id = 1;
            }
            else
            {
                openIssue.Id = OpenIssues[index - 1].Id + 1;
            }

            OpenIssues.Add(openIssue);

            return OpenIssues.Count - 1;
        }

        public void RemoveOpenIssue(OpenIssue openIssue)
        {
            foreach(OpenIssue tmpOpenIssue in OpenIssues)
            {
                if(tmpOpenIssue.Id > openIssue.Id)
                {
                    tmpOpenIssue.Id -= 1;
                }
            }
            OpenIssues.Remove(openIssue);
        }

        public OpenIssue FindOpenIssueByUniqueID(String uniqueID)
        {
            OpenIssue openIssue = null;

            foreach(OpenIssue tmpOpenIssue in OpenIssues)
            {
                if(tmpOpenIssue.UniqueId == uniqueID)
                {
                    openIssue = tmpOpenIssue;
                }
            }

            return openIssue;
        }
        #endregion

        #region ActiveActors Handling
        public void AddActiveActor(Actor actor)
        {
            ActiveActor aactor = new ActiveActor();
            aactor.ActorUniqueID = actor.UniqueId;
            aactor.IsPrimary = false;
            ActiveActors.Add(aactor);
        }

        public void RemoveActiveActor(Actor actor)
        {
            ActiveActor aactor = ActiveActors.FindByUniqueID(actor.UniqueId);
            if(aactor != null)
            {
                ActiveActors.Remove(aactor);
            }
        }
        #endregion

        #region History Item Handling
        public void AddHistoryItem(DateTime date,HistoryItem.HistoryType type,Int32 action,String notes)
        {
            HistoryItem hi = new HistoryItem();
            hi.Date = date;
            hi.Type = type;
            hi.Action = action;
            hi.Notes = notes;
            HistoryItems.Add(hi);
        }

        public void RemoveHistoryItem(Int32 index)
        {
            HistoryItems.RemoveAt(index);
        }
        #endregion
        #endregion

        #region Private Methods
        private Int32 FindStepIndexByUniqueId(String uniqueID)
        {
            Int32 ret = -1;

            for(int i = 0; i < this.Steps.Count; i++)
            {
                if(Steps[i].UniqueId == uniqueID)
                {
                    ret = i;
                }
            }

            return ret;
        }
        #endregion

        public override void PurgeReferences(Package thisPackage, Package currentPackage, string oldNameStartTag, string oldNameEndTag, string newNameStartTag, string newNameEndTag, bool doNotMark)
        {
            foreach (Package p in currentPackage.Packages)
            {
                p.PurgeReferences(
                    this,
                    p,
                    oldNameStartTag,
                    oldNameEndTag,
                    newNameStartTag,
                    newNameEndTag,
                    doNotMark);
            }
            if (!doNotMark)
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
            // remove use case references in use case's steps
            foreach (UseCase uc in thisPackage.UseCases)
            {
                foreach (Step step in uc.Steps)
                {
                    if (step.Dependency.PartnerUniqueID == this.UniqueId)
                    {
                        step.Dependency.Type = DependencyItem.ReferenceType.None;
                        step.Dependency.PartnerUniqueID = "";
                        step.Dependency.Stereotype = "";
                    }
                }
            }

        }
    }
}
