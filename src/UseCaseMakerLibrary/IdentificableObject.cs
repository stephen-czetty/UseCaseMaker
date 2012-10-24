using System;
using System.Xml;

namespace UseCaseMakerLibrary
{
	public class IdentificableObject : IIdentificableObject, IXMLNodeSerializable
	{
		#region Class Members

	    #endregion

		#region Constructors
		internal IdentificableObject()
		{
		    ObjectUserViewStatus = new UserViewStatus();
		    Prefix = String.Empty;
		    ID = -1;
		    Owner = null;
		    Name = String.Empty;
		    UniqueID = String.Empty;
		    MakeUniqueID();
		}

	    internal IdentificableObject(String name, String prefix, Int32 id)
		{
	        ObjectUserViewStatus = new UserViewStatus();
	        Owner = null;
	        UniqueID = String.Empty;
	        MakeUniqueID();
			this.Name = name;
			this.Prefix = prefix;
			this.ID = id;
		}

		internal IdentificableObject(String name, String prefix, Int32 id, Package owner)
		{
		    ObjectUserViewStatus = new UserViewStatus();
		    UniqueID = String.Empty;
		    MakeUniqueID();
			this.Name = name;
			this.Prefix = prefix;
			this.ID = id;
			this.Owner = owner;
		}
		#endregion

		#region Public Properties

	    [XMLSerializeAsAttribute]
	    public string UniqueID { get; set; }

	    [XMLSerializeIgnore]
	    public Package Owner { get; set; }

	    [XMLSerializeAsAttribute]
	    public string Name { get; set; }

	    [XMLSerializeAsAttribute]
	    public int ID { get; set; }

	    [XMLSerializeAsAttribute]
	    public string Prefix { get; set; }

	    [XMLSerializeAsAttribute(true)]
		public String Path
		{
			get
			{
				string path = this.Prefix + this.ID.ToString();
				IdentificableObject owner = this.Owner;
				while(owner != null)
				{
					path = owner.Prefix + owner.ID.ToString() + "." + path;
					owner = owner.Owner;
				}
				return path;
			}
		}

		[XMLSerializeIgnore]
		public String ElementID
		{
			get
			{
				return this.Prefix + this.ID.ToString();
			}
		}

	    [XMLSerializeIgnore]
	    public UserViewStatus ObjectUserViewStatus { get; private set; }

	    #endregion
	
		#region Private Methods
		private void MakeUniqueID()
		{
			Guid guid = Guid.NewGuid();
			this.UniqueID = guid.ToString();
		}
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
