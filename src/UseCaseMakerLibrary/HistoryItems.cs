using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Xml.Serialization;

namespace UseCaseMakerLibrary
{
	/// <summary>
	/// Descrizione di riepilogo per HistoryItems.
	/// </summary>
	[XmlInclude(typeof(HistoryItem))]
	public class HistoryItems : ICollection<HistoryItem>, IXMLNodeSerializable, ICollection, IXmlCollectionSerializable<HistoryItem>
	{
		private readonly IList<HistoryItem> _items = new List<HistoryItem>();
	    private readonly object _syncRoot = new object();

	    internal HistoryItems()
		{
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

	        foreach(HistoryItem item in _items)
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

		public void Add(HistoryItem item)
		{
		    Contract.Requires<ArgumentNullException>(item != null);
		    _items.Add(item);
		}

	    public void Clear()
		{
			_items.Clear();
		}

		public bool Contains(HistoryItem item)
		{
			return _items.Contains(item);
		}

	    public bool Remove(HistoryItem item)
		{
			return _items.Remove(item);
		}

		public void RemoveAt(int index)
		{
			_items.RemoveAt(index);
		}

		public void CopyTo(HistoryItem[] array, int index)
		{
			_items.CopyTo(array, index);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

        public IEnumerator<HistoryItem> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

	    public HistoryItem this[int idx]
	    {
            get { return _items[idx]; }
	        set {  }
	    }
	}
}
