using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace UseCaseMakerLibrary
{
	/// <summary>
	/// Descrizione di riepilogo per HistoryItems.
	/// </summary>
	public class HistoryItems : ICollection<HistoryItem>, IXMLNodeSerializable
	{
		private readonly IList<HistoryItem> _items = new List<HistoryItem>();

		internal HistoryItems()
		{
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

		public void Add(HistoryItem item)
		{
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
