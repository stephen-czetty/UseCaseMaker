using System;
using System.Xml;

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

		#region IXMLNodeSerializable Implementation
		public XmlNode XmlSerialize(XmlDocument document, object instance, string propertyName, bool deep)
		{
			return XmlSerializer.XmlSerialize(document,this,propertyName,true);
		}

		public void XmlDeserialize(XmlNode fromNode, object instance)
		{
			XmlSerializer.XmlDeserialize(fromNode,instance);
		}
		#endregion
	}
}
