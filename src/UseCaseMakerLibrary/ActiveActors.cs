using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Xml.Serialization;

namespace UseCaseMakerLibrary
{
	public class ActiveActors : ICollection<ActiveActor>, IXMLNodeSerializable, ICollection
	{
		#region Class Members
		private readonly IList<ActiveActor> _items = new List<ActiveActor>();
	    private readonly object _syncRoot = new object();

	    #endregion

		#region Constructors
		internal ActiveActors()
		{
		}
		#endregion

		#region Public Properties

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

            foreach (ActiveActor item in _items)
            {
                array.SetValue(item, index++);
            }
	    }

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
		public ActiveActor this[int index]
		{
			get
			{
				return _items[index];
			}
		}
		#endregion

		#region Public Methods
		public void Add(ActiveActor item)
		{
			_items.Add(item);
		}

		public void Clear()
		{
			_items.Clear();
		}

		public bool Contains(ActiveActor item)
		{
			return _items.Contains(item);
		}

	    public void CopyTo(ActiveActor[] array, int arrayIndex)
	    {
	        _items.CopyTo(array, arrayIndex);
	    }

	    public int IndexOf(ActiveActor item)
		{
			return _items.IndexOf(item);
		}

		public void Insert(int index, ActiveActor item)
		{
			_items.Insert(index, item);
		}

		public bool Remove(ActiveActor item)
		{
			return _items.Remove(item);
		}

		public void RemoveAt(int index)
		{
			_items.RemoveAt(index);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
		    return GetEnumerator();
		}

        public IEnumerator<ActiveActor> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

		public ActiveActor FindByUniqueID(String uniqueID)
		{
			ActiveActor aactor = null;
			foreach(ActiveActor tmpAActor in this._items)
			{
				if(tmpAActor.ActorUniqueID == uniqueID)
				{
					aactor = tmpAActor;
				}
			}

			return aactor;
		}
		#endregion
	}
}
