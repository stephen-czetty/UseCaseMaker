using System;

namespace UseCaseMakerLibrary
{
	public class OpenIssue : IdentificableObject
	{
		#region Public Constants and Enumerators
		#endregion

		#region Class Members

	    #endregion

		#region Constructors
		internal OpenIssue()
		{
		    Description = String.Empty;
		}

	    #endregion

		#region Public Properties

	    public string Description { get; set; }

		#endregion
	}
}
