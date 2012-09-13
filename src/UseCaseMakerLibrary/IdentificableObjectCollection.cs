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
    public abstract class IdentificableObjectCollection<T> : IdentificableObject, ICollection<T>
        where T : class, IIdentificableObject
	{
		private readonly IList<T> _items = new List<T>();

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
	        get { return _items.IsReadOnly; }
	    }

		[XMLSerializeIgnore]
		public T this[int index]
		{
			get
			{
				return _items[index];
			}
		}

	

		[XMLSerializeAsAttribute(true)]
		public override String Path
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
		public override String ElementID
		{
			get
			{
				return Owner.ElementID;
			}
		}

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
	        return _items.Contains(item);
	    }

	    public void CopyTo(T[] array, int arrayIndex)
	    {
	        _items.CopyTo(array, arrayIndex);
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

		public T FindByUniqueID(String uniqueID)
		{
			T element = null;
			foreach(T tmpElement in this)
			{
				if(tmpElement.UniqueID == uniqueID)
				{
					element = tmpElement;
				}
			}

			return element;
		}

		public T FindByElementID(String elementID)
		{
			T element = null;
			foreach(T tmpElement in this)
			{
				if(tmpElement.ElementID == elementID)
				{
					element = tmpElement;
				}
			}

			return element;
		}

		public T FindByPath(String path)
		{
			T element = null;
			foreach(T tmpElement in _items)
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
			foreach(T tmpElement in _items)
			{
				if(tmpElement.ID > id)
				{
					id = tmpElement.ID;
				}
			}

			return (id + 1);
		}

		#region IXMLNodeSerializable Implementation
		public override XmlNode XmlSerialize(XmlDocument document, object instance, string propertyName, bool deep)
		{
			return XmlSerializer.XmlSerialize(document,this,propertyName,true);
		}

		public override void XmlDeserialize(XmlNode fromNode, object instance)
		{
			XmlSerializer.XmlDeserialize(fromNode,instance);
		}
		#endregion
	}
}
