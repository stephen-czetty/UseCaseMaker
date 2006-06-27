using System;

namespace UseCaseMakerLibrary
{
	public class Actor : IdentificableObject
	{
		private CommonAttributes commonAttributes = new CommonAttributes();

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

		#region Static Members
		#endregion
	}
}
