using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace UseCaseMakerLibrary
{
	/// <summary>
	/// Descrizione di riepilogo per IdentificableObjectCollection.
	/// </summary>
	public abstract class IdentificableObjectCollection<T> : ICollection<T>, IIdentificableObject, IXMLNodeSerializable
        where T : IIdentificableObject
	{
		#region Class Members
		private readonly IList<T> _items = new List<T>();
		private readonly IdentificableObject _ia = new IdentificableObject();
		#endregion

		#region Public Properties

	    public bool Remove(T item)
	    {
	        return _items.Remove(item);
	    }

	    /// <summary>
		/// Returns the number of elements in the MenuItemCollection
		/// </summary>
		[XMLSerializeIgnore]
		public int Count
		{
			get
			{
				return _items.Count;
			}
		}

	    public bool IsReadOnly
	    {
	        get { throw new NotImplementedException(); }
	    }

		[XMLSerializeIgnore]
		public T this[int index]
		{
			get
			{
				return _items[index];
			}
		}

		#region IIdentificableObject implementation
		[XMLSerializeAsAttribute]
		public String UniqueID
		{
			get
			{
				return this._ia.UniqueID;
			}
			set
			{
				this._ia.UniqueID = value;
			}
		}

		[XMLSerializeIgnore]
		public Package Owner
		{
			get
			{
				return this._ia.Owner;
			}
			set
			{
				this._ia.Owner = value;
			}
		}

		[XMLSerializeAsAttribute]
		public String Name
		{
			get
			{
				return this._ia.Name;
			}
			set
			{
				this._ia.Name = value;
			}
		}

		[XMLSerializeAsAttribute]
		public Int32 ID
		{
			get
			{
				return this._ia.ID;
			}
			set
			{
				this._ia.ID = value;
			}
		}

		[XMLSerializeAsAttribute]
		public String Prefix
		{
			get
			{
				return this._ia.Prefix;
			}
			set
			{
				this._ia.Prefix = value;
			}
		}

		[XMLSerializeAsAttribute(true)]
		public String Path
		{
			get
			{
				if(Owner == null)
				{
					return string.Empty;
				}
				return Owner.Path;
			}
		}

		[XMLSerializeIgnore]
		public String ElementID
		{
			get
			{
				return Owner.ElementID;
			}
		}

	    public void PurgeReferences(Package thisPackage, Package currentPackage, string oldNameStartTag, string oldNameEndTag, string newNameStartTag, string newNameEndTag, bool dontMark)
	    {
	    }

	    #endregion
		#endregion

		#region Public Methods
		public void Add(T item)
		{
             _items.Add(item);
		}

        public void Insert(int index, T item)
        {
            _items.Insert(index, item);
        }

	    public void Clear()
		{
			_items.Clear();
		}

	    public bool Contains(T item)
	    {
	        throw new NotImplementedException();
	    }

	    public void CopyTo(T[] array, int arrayIndex)
	    {
	        throw new NotImplementedException();
	    }

	    public IEnumerator<T> GetEnumerator()
	    {
	        return _items.GetEnumerator();
	    }

	    IEnumerator IEnumerable.GetEnumerator()
	    {
	        return GetEnumerator();
	    }

        public ICollection<T> Sorted(string propertyName)
        {
            var ps = new PropertySorter<T>(propertyName, "ASC");
            return _items.OrderBy(x => x, ps).ToList();
        }

		public IIdentificableObject FindByName(String name)
		{
			IIdentificableObject element = null;
			foreach(T tmpElement in this)
			{
				if(tmpElement.Name == name)
				{
					element = tmpElement;
				}
			}

			return element;
		}

		public IIdentificableObject FindByUniqueID(String uniqueID)
		{
			IIdentificableObject element = null;
			foreach(T tmpElement in this)
			{
				if(tmpElement.UniqueID == uniqueID)
				{
					element = tmpElement;
				}
			}

			return element;
		}

		public object FindByElementID(String elementID)
		{
			IIdentificableObject element = null;
			foreach(T tmpElement in this)
			{
				if(tmpElement.ElementID == elementID)
				{
					element = tmpElement;
				}
			}

			return element;
		}

		public object FindByPath(String path)
		{
			IIdentificableObject element = null;
			foreach(IIdentificableObject tmpElement in this)
			{
				if(tmpElement.Path == path)
				{
					element = tmpElement;
				}
			}

			return element;
		}

		public Int32 GetNextFreeID()
		{
			int id = 0;
			foreach(IIdentificableObject tmpElement in this)
			{
				if(tmpElement.ID > id)
				{
					id = tmpElement.ID;
				}
			}

			return (id + 1);
		}
		#endregion

		#region Protected Methods
		#endregion

		#region Private Methods
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
