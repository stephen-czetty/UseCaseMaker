using System;
using System.Xml.Serialization;
using UseCaseMakerLibrary.Contracts;

namespace UseCaseMakerLibrary
{
    /// <summary>
    /// An object that can be identified by a number of different properties
    /// </summary>
    public abstract class IdentificableObject : IIdentificableObject, IXMLNodeSerializable
    {
        /// <summary>
        /// The name of the object
        /// </summary>
        private string _name;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentificableObject"/> class.
        /// </summary>
        internal IdentificableObject() : this(string.Empty, string.Empty, -1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentificableObject"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="id">The id.</param>
        /// <param name="owner">The owner.</param>
        internal IdentificableObject(string name, string prefix, int id, Package owner = null)
        {
            ObjectUserViewStatus = new UserViewStatus();
            UniqueId = string.Empty;
            MakeUniqueId();
            _name = name;
            Prefix = prefix;
            Id = id;
            Owner = owner;
        }
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the unique ID.
        /// </summary>
        [XmlAttribute]
        public string UniqueId { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        [XmlIgnore]
        public Package Owner { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [XmlAttribute]
        public virtual string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        [XmlAttribute]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the prefix.
        /// </summary>
        /// <value>
        /// The prefix.
        /// </value>
        [XmlAttribute]
        public string Prefix { get; set; }

        /// <summary>
        /// Gets the path.
        /// </summary>
        [XmlIgnore]
        public virtual string Path
        {
            get
            {
                string path = this.ElementId;
                IdentificableObject owner = Owner;
                while (owner != null)
                {
                    path = owner.ElementId + "." + path;
                    owner = owner.Owner;
                }

                return path;
            }
        }

        /// <summary>
        /// Gets the element ID.
        /// </summary>
        [XmlIgnore]
        public virtual string ElementId
        {
            get
            {
                return Prefix + Id;
            }
        }

        /// <summary>
        /// Gets the object user view status.
        /// </summary>
        [XmlIgnore]
        public UserViewStatus ObjectUserViewStatus { get; private set; }

        /// <summary>
        /// Purges the references.
        /// </summary>
        /// <param name="thisPackage">The this package.</param>
        /// <param name="currentPackage">The current package.</param>
        /// <param name="oldNameStartTag">The old name start tag.</param>
        /// <param name="oldNameEndTag">The old name end tag.</param>
        /// <param name="newNameStartTag">The new name start tag.</param>
        /// <param name="newNameEndTag">The new name end tag.</param>
        /// <param name="doNotMark">if set to <c>true</c> [do not mark].</param>
        public virtual void PurgeReferences(Package thisPackage, Package currentPackage, string oldNameStartTag, string oldNameEndTag, string newNameStartTag, string newNameEndTag, bool doNotMark)
        {
        }

        #endregion
    
        #region Private Methods
        /// <summary>
        /// Makes the unique id.
        /// </summary>
        private void MakeUniqueId()
        {
            Guid guid = Guid.NewGuid();
            UniqueId = guid.ToString();
        }
        #endregion
    }
}
