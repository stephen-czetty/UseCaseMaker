using System;

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
			: base()
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

	    internal UseCase(String name, String prefix, Int32 id)
			: base(name,prefix,id)
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

	    internal UseCase(String name, String prefix, Int32 id, Package owner)
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

	    public CommonAttributes Attributes { get; private set; }

	    public Steps Steps { get; private set; }

	    public OpenIssues OpenIssues { get; private set; }

	    public ActiveActors ActiveActors { get; private set; }

	    public HistoryItems HistoryItems { get; private set; }

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
				step.Dependency.PartnerUniqueID = referencedUseCase.UniqueID;
				step.Dependency.Type = referenceType;
				step.Description = (step.Dependency.Stereotype != "") ? "<<" + step.Dependency.Stereotype + ">>" : "";
				step.Description += " \"";
				step.Description += referencedUseCase.Name;
				step.Description += "\"";
			}

			if(previousStep == null)
			{
				step.ID = 1;
				ret = this.Steps.Add(step);
				return ret;
			}
			else
			{
				switch(type)
				{
					case Step.StepType.Default:
						if(previousStep.Type == Step.StepType.Default)
						{
							step.ID = previousStep.ID;
							index = this.FindStepIndexByUniqueID(previousStep.UniqueID) + 1;
							while(true)
							{
								if(index == this.Steps.Count)
								{
									previousStep = (Step)this.Steps[index - 1];
									break;
								}
								Step tmpStep = (Step)this.Steps[index];
								if(tmpStep.ID != step.ID)
								{
									previousStep = (Step)this.Steps[index - 1];
									break;
								}
								index += 1;
							}
							step.ID = previousStep.ID + 1;
							foreach(Step tmpStep in this.Steps)
							{
								if(tmpStep.ID >= step.ID)
								{
									tmpStep.ID += 1;
								}
							}
						}
						else if(previousStep.Type == Step.StepType.Alternative)
						{
							step.ID = previousStep.ID;
							step.Type = Step.StepType.Alternative;

							index = this.FindStepIndexByUniqueID(previousStep.UniqueID) + 1;
							while(true)
							{
								if(index == this.Steps.Count)
								{
									previousStep = (Step)this.Steps[index - 1];
									break;
								}
								Step tmpStep = (Step)this.Steps[index];
								if(tmpStep.ID != step.ID || tmpStep.Prefix == String.Empty)
								{
									previousStep = (Step)this.Steps[index - 1];
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
						
							foreach(Step tmpStep in this.Steps)
							{
								if(tmpStep.ID == step.ID)
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
							step.ID = previousStep.ID;
							step.Prefix = previousStep.Prefix;

							index = this.FindStepIndexByUniqueID(previousStep.UniqueID) + 1;
							while(true)
							{
								if(index == this.Steps.Count)
								{
									previousStep = (Step)this.Steps[index - 1];
									break;
								}
								Step tmpStep = (Step)this.Steps[index];
								if(tmpStep.ID != step.ID || tmpStep.Prefix != step.Prefix)
								{
									previousStep = (Step)this.Steps[index - 1];
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
							step.ID = previousStep.ID;
							step.Type = Step.StepType.Alternative;

							index = this.FindStepIndexByUniqueID(previousStep.UniqueID) + 1;
							while(true)
							{
								if(index == this.Steps.Count)
								{
									previousStep = (Step)this.Steps[index - 1];
									break;
								}
								Step tmpStep = (Step)this.Steps[index];
								if(tmpStep.ID != step.ID || tmpStep.Prefix == String.Empty)
								{
									previousStep = (Step)this.Steps[index - 1];
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
						
							foreach(Step tmpStep in this.Steps)
							{
								if(tmpStep.ID == step.ID)
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
							step.ID = previousStep.ID;
							step.Prefix = previousStep.Prefix;
							step.ChildID = 1;
						}
						break;
				}
			}

			index = this.FindStepIndexByUniqueID(previousStep.UniqueID) + 1;
			if(index == this.Steps.Count)
			{
				ret = this.Steps.Add(step);
			}
			else
			{
				this.Steps.Insert(index, step);
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
				step.Dependency.PartnerUniqueID = referencedUseCase.UniqueID;
				step.Dependency.Type = referenceType;
				step.Description = (step.Dependency.Stereotype != "") ? "<<" + step.Dependency.Stereotype + ">>" : "";
				step.Description += " \"";
				step.Description += referencedUseCase.Name;
				step.Description += "\"";
			}

			if(previousStep.Type == Step.StepType.Default)
			{
				step.ID = previousStep.ID;
				foreach(Step tmpStep in this.Steps)
				{
					if(tmpStep.ID >= step.ID)
					{
						tmpStep.ID += 1;
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
				step.ID = previousStep.ID;
				step.Type = Step.StepType.Alternative;

				foreach(Step tmpStep in this.Steps)
				{
					if(tmpStep.ID == step.ID)
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
				step.ID = previousStep.ID;
				step.Prefix = previousStep.Prefix;
				step.ChildID = previousStep.ChildID;
				foreach(Step tmpStep in this.Steps)
				{
					if(tmpStep.ID == step.ID && tmpStep.Prefix == step.Prefix)
					{
						if(tmpStep.ChildID >= step.ChildID)
						{
							tmpStep.ChildID += 1;
						}
					}
				}
			}

			int index = this.FindStepIndexByUniqueID(previousStep.UniqueID);
			this.Steps.Insert(index,step);
			ret = index;

			return ret;
		}

		public void RemoveStep(Step step)
		{
			for(int i = this.Steps.Count - 1; i >= 0; i--)
			{
				Step tmpStep = (Step)this.Steps[i];
				switch(step.Type)
				{
					case Step.StepType.Default:
						if(tmpStep.ID == step.ID)
						{
							this.Steps.Remove(tmpStep);
						}
						if(tmpStep.ID > step.ID)
						{
							tmpStep.ID -= 1;
						}
						break;
					case Step.StepType.Alternative:
						if(tmpStep.ID == step.ID)
						{
							if(tmpStep.Prefix == step.Prefix)
							{
								this.Steps.Remove(tmpStep);
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
						if(tmpStep.ID == step.ID && tmpStep.Prefix == step.Prefix)
						{
							if(tmpStep.ChildID == step.ChildID)
							{
								this.Steps.Remove(step);
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

			foreach(Step tmpStep in this.Steps)
			{
				if(tmpStep.UniqueID == uniqueID)
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

			for(index = this.Steps.Count - 1; index >= 0; index--)
			{
				tmpStep = (Step)this.Steps[index];
				if(tmpStep.UniqueID == step.UniqueID)
				{
					break;
				}
			}
			// Step is not found or it is the last step in collection
			if(index >= this.Steps.Count - 1)
			{
				return false;
			}
			tmpStep = (Step)this.Steps[index + 1];
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
			Int32 index = this.OpenIssues.Count;
			Int32 ret;

			if(index == 0)
			{
				openIssue.ID = 1;
			}
			else
			{
				openIssue.ID = ((OpenIssue)this.OpenIssues[index - 1]).ID + 1;
			}

			ret = this.OpenIssues.Add(openIssue);

			return ret;
		}

		public void RemoveOpenIssue(OpenIssue openIssue)
		{
			foreach(OpenIssue tmpOpenIssue in this.OpenIssues)
			{
				if(tmpOpenIssue.ID > openIssue.ID)
				{
					tmpOpenIssue.ID -= 1;
				}
			}
			this.OpenIssues.Remove(openIssue);
		}

		public OpenIssue FindOpenIssueByUniqueID(String uniqueID)
		{
			OpenIssue openIssue = null;

			foreach(OpenIssue tmpOpenIssue in this.OpenIssues)
			{
				if(tmpOpenIssue.UniqueID == uniqueID)
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
			aactor.ActorUniqueID = actor.UniqueID;
			aactor.IsPrimary = false;
			this.ActiveActors.Add(aactor);
		}

		public void RemoveActiveActor(Actor actor)
		{
			ActiveActor aactor = (ActiveActor)this.ActiveActors.FindByUniqueID(actor.UniqueID);
			if(aactor != null)
			{
				this.ActiveActors.Remove(aactor);
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
			this.HistoryItems.Add(hi);
		}

		public void RemoveHistoryItem(Int32 index)
		{
			this.HistoryItems.RemoveAt(index);
		}
		#endregion
		#endregion

		#region Private Methods
		private Int32 FindStepIndexByUniqueID(String uniqueID)
		{
			Int32 ret = -1;

			for(int i = 0; i < this.Steps.Count; i++)
			{
				if(((Step)this.Steps[i]).UniqueID == uniqueID)
				{
					ret = i;
				}
			}

			return ret;
		}
		#endregion
	}
}
