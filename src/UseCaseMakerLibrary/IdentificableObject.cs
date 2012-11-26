using System;
using System.Xml.Serialization;
using UseCaseMakerLibrary.Contracts;

namespace UseCaseMakerLibrary
{
    public abstract class IdentificableObject : IIdentificableObject, IXMLNodeSerializable
    {
        #region Constructors
        internal IdentificableObject()
        {
            ObjectUserViewStatus = new UserViewStatus();
            Prefix = String.Empty;
            this.Id = -1;
            this.UniqueId = String.Empty;
            Name = String.Empty;
            Owner = null;
            MakeUniqueId();
        }

		internal IdentificableObject(String name, String prefix, Int32 id)
        {
	        ObjectUserViewStatus = new UserViewStatus();
	        this.UniqueId = String.Empty;
            Owner = null;
            MakeUniqueId();
            Name = name;
            Prefix = prefix;
            this.Id = id;
        }

        internal IdentificableObject(String name, String prefix, Int32 id, Package owner)
        {
            ObjectUserViewStatus = new UserViewStatus();
            this.UniqueId = String.Empty;
            MakeUniqueId();
            Name = name;
            Prefix = prefix;
            this.Id = id;
            Owner = owner;
        }
        #endregion

        #region Public Properties

        [XmlAttribute]
	    public string UniqueId { get; set; }

        [XmlIgnore]
	    public Package Owner { get; set; }

        [XmlAttribute]
	    public virtual string Name { get; set; }

        [XmlAttribute]
	    public int Id { get; set; }

        [XmlAttribute]
        public string Prefix { get; set; }

        [XmlIgnore]
        public virtual String Path
        {
            get
            {
                string path = this.ElementId;
                IdentificableObject owner = Owner;
                while(owner != null)
                {
                    path = owner.ElementId + "." + path;
                    owner = owner.Owner;
                }
                return path;
            }
        }

        [XmlIgnore]
        public virtual String ElementId
        {
            get
            {
				return Prefix + this.Id.ToString();
            }
        }

        public virtual void PurgeReferences(Package thisPackage, Package currentPackage, string oldNameStartTag, string oldNameEndTag, string newNameStartTag, string newNameEndTag, bool doNotMark)
        {
        }
        [XmlIgnore]
        public UserViewStatus ObjectUserViewStatus { get; private set; }

		#endregion
    
        #region Private Methods
        private void MakeUniqueId()
        {
            Guid guid = Guid.NewGuid();
            this.UniqueId = guid.ToString();
        }
        #endregion
    }
}
