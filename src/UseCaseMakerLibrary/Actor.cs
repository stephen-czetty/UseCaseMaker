using System;

namespace UseCaseMakerLibrary
{
	public class Actor : IdentificableObject
	{
		#region Class Members
		private CommonAttributes commonAttributes = new CommonAttributes();
		private Goals goals = new Goals();
		#endregion

		#region Constructors
		internal Actor()
			: base()
		{
		}

		internal Actor(String name, String prefix, Int32 id)
			: base(name,prefix,id)
		{
		}

		internal Actor(String name, String prefix, Int32 id, Package owner)
			: base(name,prefix,id,owner)
		{
		}
		#endregion

		#region Public Properties
		public CommonAttributes Attributes
		{
			get
			{
				return this.commonAttributes;
			}
		}
		#endregion

		#region Public Properties
		public Goals Goals
		{
			get
			{
				return this.goals;
			}
		}
		#endregion

		#region Public Methods
		#region Goals Handling
		public Int32 AddGoal()
		{
			Goal goal = new Goal();
			Int32 index = this.goals.Count;

		    if(index == 0)
			{
				goal.ID = 1;
			}
			else
			{
				goal.ID = goals[index - 1].ID + 1;
			}

			goals.Add(goal);

		    return goals.Count - 1;
		}

		public void RemoveGoal(Goal goal)
		{
			foreach(Goal tmpGoal in this.goals)
			{
				if(tmpGoal.ID > goals.ID)
				{
					tmpGoal.ID -= 1;
				}
			}
			this.goals.Remove(goal);
		}

		public Goal FindGoalByUniqueID(String uniqueID)
		{
			Goal goal = null;

			foreach(Goal tmpGoal in this.goals)
			{
				if(tmpGoal.UniqueID == uniqueID)
				{
					goal = tmpGoal;
				}
			}

			return goal;
		}
		#endregion
		#endregion

        public override void PurgeReferences(Package thisPackage, Package currentPackage, string oldNameStartTag, string oldNameEndTag, string newNameStartTag, string newNameEndTag, bool dontMark)
        {
            foreach(UseCase uc in thisPackage.UseCases)
		        {
		            ActiveActor tmpAActor = null;
		            foreach(ActiveActor aactor in uc.ActiveActors)
		            {
		                if(aactor.ActorUniqueID == UniqueID)
		                {
		                    tmpAActor = aactor;
		                }
		            }
		            if(tmpAActor != null)
		            {
		                uc.ActiveActors.Remove(tmpAActor);
		            }
		        }
		        foreach(Package p in thisPackage.Packages)
		        {
		            p.PurgeReferences(
		                this,
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
        

		#region Static Members
		#endregion
	}
}
