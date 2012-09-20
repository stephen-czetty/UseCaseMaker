using System;
using System.Xml;
using System.Xml.Serialization;

namespace UseCaseMakerLibrary
{
	public abstract class IdentificableObject : IIdentificableObject, IXMLNodeSerializable
	{
		#region Constructors
		internal IdentificableObject()
		{
		    ObjectUserViewStatus = new UserViewStatus();
		    Prefix = String.Empty;
		    ID = -1;
		    UniqueID = String.Empty;
		    Name = String.Empty;
		    Owner = null;
		    MakeUniqueId();
		}

	    internal IdentificableObject(String name, String prefix, Int32 id)
		{
	        ObjectUserViewStatus = new UserViewStatus();
	        UniqueID = String.Empty;
	        Owner = null;
	        MakeUniqueId();
			Name = name;
			Prefix = prefix;
			ID = id;
		}

		internal IdentificableObject(String name, String prefix, Int32 id, Package owner)
		{
		    ObjectUserViewStatus = new UserViewStatus();
		    UniqueID = String.Empty;
		    MakeUniqueId();
			Name = name;
			Prefix = prefix;
			ID = id;
			Owner = owner;
		}
		#endregion

		#region Public Properties

        [XmlAttribute]
	    public string UniqueID { get; set; }

        [XmlIgnore]
	    public Package Owner { get; set; }

        [XmlAttribute]
	    public string Name { get; set; }

        [XmlAttribute]
	    public int ID { get; set; }

        [XmlAttribute]
	    public string Prefix { get; set; }

        [XmlIgnore]
		public virtual String Path
		{
			get
			{
				string path = ElementID;
				IdentificableObject owner = Owner;
				while(owner != null)
				{
					path = owner.ElementID + "." + path;
					owner = owner.Owner;
				}
				return path;
			}
		}

        [XmlIgnore]
		public virtual String ElementID
		{
			get
			{
				return Prefix + ID;
			}
		}

	    public virtual void PurgeReferences(Package thisPackage, Package currentPackage, string oldNameStartTag, string oldNameEndTag, string newNameStartTag, string newNameEndTag, bool dontMark)
	    {
	    }

        [XmlIgnore]
	    public UserViewStatus ObjectUserViewStatus { get; private set; }

	    #endregion
	
		#region Private Methods
		private void MakeUniqueId()
		{
			Guid guid = Guid.NewGuid();
			UniqueID = guid.ToString();
		}
		#endregion
	}
}
