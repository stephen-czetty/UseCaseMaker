using System;
using System.Xml;

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

	    public string ActorUniqueID { get; set; }

	    public bool IsPrimary { get; set; }

	    #endregion

		#region IXMLNodeSerializable Implementation
		public XmlNode XmlSerialize(XmlDocument document, object instance, string propertyName, bool deep)
		{
			return XmlSerializer.XmlSerialize(document,this,propertyName,deep);
		}

		public void XmlDeserialize(XmlNode fromNode, object instance)
		{
			XmlSerializer.XmlDeserialize(fromNode,instance);
		}
		#endregion
	}
}
