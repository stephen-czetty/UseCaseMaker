using System;

namespace UseCaseMakerLibrary
{
	/// <summary>
	/// Descrizione di riepilogo per RelatedDocument.
	/// </summary>
	public class RelatedDocument : IXMLNodeSerializable
	{
		#region Class Members

	    public RelatedDocument()
	    {
	        FileName = String.Empty;
	    }

	    #endregion

		#region Constructors

	    #endregion

		#region Public Properties

	    public string FileName { get; set; }

	    #endregion
	}
}
