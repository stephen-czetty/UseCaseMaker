using System;

namespace UseCaseMakerLibrary
{
	public class Actors : IdentificableObjectCollection<Actor>
	{
		internal Actors(Package owner)
		{
			base.Owner = owner;
		}

		#region Public Properties
		#endregion

		#region Public Methods
		#endregion
	}
}
