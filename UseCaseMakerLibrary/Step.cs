using System;

namespace UseCaseMakerLibrary
{
	public class Step : IdentificableObject
	{
		#region Public Constants and Enumerators
		public enum StepType
		{
			Default = 0,
			Alternative = 1,
			AlternativeChild = 2,
			UseCaseReference = 3
		}

		public enum StepReferenceType
		{
			None = 0,
			Client = 1,
			Supplier = 2
		}
		#endregion

		#region Class Members
		private String description = String.Empty;
		private Int32 childID = -1;
		private StepType type = StepType.Default;
		private String referenceStereotype = "";
		private StepReferenceType referenceType = StepReferenceType.None;
		private String referencedUseCaseUniqueID = "";
		#endregion

		#region Constructors
		internal Step()
		{
		}
		#endregion

		#region Public Properties
		public String Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		public Int32 ChildID
		{
			get
			{
				return this.childID;
			}
			set
			{
				this.childID = value;
			}
		}

		public StepType Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		public String ReferenceStereotipe
		{
			get
			{
				return this.referenceStereotype;
			}
			set
			{
				this.referenceStereotype = value;
			}
		}

		public StepReferenceType ReferenceType
		{
			get
			{
				return this.referenceType;
			}
			set
			{
				this.referenceType = value;
				if(value == StepReferenceType.None)
				{
					this.referencedUseCaseUniqueID = "";
					this.referenceStereotype = "";
				}
			}
		}

		public String ReferencedUseCaseUniqueID
		{
			get
			{
				return this.referencedUseCaseUniqueID;
			}
			set
			{
				if(this.referenceType != StepReferenceType.None)
				{
					this.referencedUseCaseUniqueID = value;
				}
			}
		}

		public new String UniqueID
		{
			get
			{
				return base.UniqueID;
			}
			set
			{
				base.UniqueID = value;
			}
		}

		public new String Name
		{
			get
			{
				return this.ID.ToString() + 
					((this.Prefix != String.Empty) ? "." + this.Prefix : String.Empty) + 
					((this.ChildID != -1) ? "." + this.ChildID.ToString() :  String.Empty);
			}
			set
			{
				base.Name = value;
			}
		}
		#endregion
	}
}
