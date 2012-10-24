using System;
using System.Xml;

namespace UseCaseMakerLibrary
{
	public class CommonAttributes : IXMLNodeSerializable
	{
		#region Class Members

	    public CommonAttributes()
	    {
	        RelatedDocuments = new RelatedDocuments();
	        Notes = String.Empty;
	        Description = String.Empty;
	    }

	    #endregion

		#region Constructors
		#endregion

		#region Public Properties

	    public string Description { get; set; }

	    public string Notes { get; set; }

	    public RelatedDocuments RelatedDocuments { get; private set; }

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
