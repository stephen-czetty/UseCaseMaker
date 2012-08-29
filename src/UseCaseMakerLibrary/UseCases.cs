using System;

namespace UseCaseMakerLibrary
{
	public class UseCases : IdentificableObjectCollection<UseCase>
	{
		internal UseCases(Package owner)
		{
			base.Owner = owner;
		}

		#region Public Properties
		#endregion

		#region Public Methods
		#endregion
	}
}
