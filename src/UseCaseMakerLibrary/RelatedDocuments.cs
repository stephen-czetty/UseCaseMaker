using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace UseCaseMakerLibrary
{
	public class RelatedDocuments : ICollection<RelatedDocument>, IXMLNodeSerializable
	{
	    private readonly IList<RelatedDocument> _items = new List<RelatedDocument>();

		internal RelatedDocuments()
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

	    [XMLSerializeIgnore]
		public object this[int index]
		{
			get
			{
				return _items[index];
			}
		}

		public void Add(RelatedDocument item)
		{
			_items.Add(item);
		}

	    public void Clear()
		{
			_items.Clear();
		}

		public bool Contains(RelatedDocument item)
		{
			return _items.Contains(item);
		}

	    public bool Remove(RelatedDocument item)
		{
			return _items.Remove(item);
		}

		public void RemoveAt(int index)
		{
			_items.RemoveAt(index);
		}

		public void CopyTo(RelatedDocument[] array, int index)
		{
			_items.CopyTo(array, index);
		}

	    public IEnumerator<RelatedDocument>GetEnumerator()
	    {
	        return _items.GetEnumerator();
	    }

	    IEnumerator IEnumerable.GetEnumerator()
	    {
	        return GetEnumerator();
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
