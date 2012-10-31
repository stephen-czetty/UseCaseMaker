using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace UseCaseMakerLibrary
{
	/// <summary>
	/// Descrizione di riepilogo per IdentificableObjectCollection.
	/// </summary>
    public abstract class IdentificableObjectCollection<T> : IdentificableObject, ICollection<T>, ICollection
        where T : class, IIdentificableObject
	{
		private readonly IList<T> _items = new List<T>();
	    private readonly object _syncRoot = new object();

	    public bool Remove(T item)
	    {
	        return _items.Remove(item);
	    }

	    /// <summary>
	    /// Copies the elements of the <see cref="T:System.Collections.ICollection"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
	    /// </summary>
	    /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection"/>. The <see cref="T:System.Array"/> must have zero-based indexing. </param><param name="index">The zero-based index in <paramref name="array"/> at which copying begins. </param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero. </exception><exception cref="T:System.ArgumentException"><paramref name="array"/> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.ICollection"/> is greater than the available space from <paramref name="index"/> to the end of the destination <paramref name="array"/>.-or-The type of the source <see cref="T:System.Collections.ICollection"/> cannot be cast automatically to the type of the destination <paramref name="array"/>.</exception><filterpriority>2</filterpriority>
	    void ICollection.CopyTo(Array array, int index)
	    {
            Contract.Requires<ArgumentNullException>(array != null);
            Contract.Requires<ArgumentOutOfRangeException>(index >= 0);
            if (array.Rank > 1 || array.Length < _items.Count + index)
                throw new ArgumentException("array");

            foreach (T item in _items)
            {
                array.SetValue(item, index++);
            }
	    }

	    /// <summary>
		/// Returns the number of elements in the MenuItemCollection
		/// </summary>
        [XmlIgnore]
		public int Count
		{
			get
			{
				return _items.Count;
			}
		}

	    /// <summary>
	    /// Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.
	    /// </summary>
	    /// <returns>
	    /// An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.
	    /// </returns>
	    /// <filterpriority>2</filterpriority>
	    object ICollection.SyncRoot
	    {
            get { return _syncRoot; }
	    }

	    /// <summary>
	    /// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe).
	    /// </summary>
	    /// <returns>
	    /// true if access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe); otherwise, false.
	    /// </returns>
	    /// <filterpriority>2</filterpriority>
	    bool ICollection.IsSynchronized
	    {
	        get { return false; }
	    }

        [XmlIgnore]
	    public bool IsReadOnly
	    {
	        get { return _items.IsReadOnly; }
	    }

        [XmlIgnore]
		public T this[int index]
		{
			get
			{
				return _items[index];
			}
		}

        [XmlIgnore]
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

        [XmlIgnore]
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
	}
}
