using System;

namespace UseCaseMakerLibrary
{
	/// <summary>
	/// Descrizione di riepilogo per RelatedDocument.
	/// </summary>
	public class RelatedDocument : IXMLNodeSerializable
	{
		#region Class Members
		String fileName = String.Empty;
		#endregion

		#region Constructors
		public RelatedDocument()
		{
		}
		#endregion

		#region Public Properties
		public String FileName
		{
			get
			{
				return this.fileName;
			}
			set
			{
				this.fileName = value;
			}
		}
		#endregion
	}
}
