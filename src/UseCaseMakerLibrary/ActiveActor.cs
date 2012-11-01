using System;

namespace UseCaseMakerLibrary
{
	public class ActiveActor : IXMLNodeSerializable
	{
		#region Class Members

	    #endregion

		#region Constructors
		internal ActiveActor()
		{
		    IsPrimary = false;
		    ActorUniqueID = String.Empty;
		}

	    #endregion

		#region Public Properties

	    public String ActorUniqueID { get; set; }

	    public Boolean IsPrimary { get; set; }

		#endregion
	}
}
