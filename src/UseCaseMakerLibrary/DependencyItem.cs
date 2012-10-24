using System;
using System.Xml;

namespace UseCaseMakerLibrary
{
	/// <summary>
	/// Descrizione di riepilogo per DependencyItem.
	/// </summary>
	public class DependencyItem : IXMLNodeSerializable
	{
		#region Public Constants and Enumerators
		public enum ReferenceType
		{
			None = 0,
			Client = 1,
			Supplier = 2
		}
		#endregion

		#region Class Members

	    #endregion

		#region Constructors
		public DependencyItem()
		{
		    PartnerUniqueID = "";
		    Type = ReferenceType.None;
		    Stereotype = "";
		    //
			// TODO: aggiungere qui la logica del costruttore
			//
		}

	    #endregion

		#region Public Properties

	    public string Stereotype { get; set; }

	    public ReferenceType Type { get; set; }

	    public string PartnerUniqueID { get; set; }

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
