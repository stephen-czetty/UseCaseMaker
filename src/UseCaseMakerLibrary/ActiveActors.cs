using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace UseCaseMakerLibrary
{
	public class ActiveActors : ICollection<ActiveActor>, IXMLNodeSerializable
	{
		#region Class Members
		private readonly IList<ActiveActor> _items = new List<ActiveActor>();
		#endregion

		#region Constructors
		internal ActiveActors()
		{
		}
		#endregion

		#region Public Properties
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

		#region IXMLNodeSerializable Implementation
		public XmlNode XmlSerialize(XmlDocument document, object instance, string propertyName, bool deep)
		{
			return XmlSerializer.XmlSerialize(document,this,propertyName,deep);
		}

		public void XmlDeserialize(XmlNode fromNode, object instance)
		{
			XmlSerializer.XmlDeserialize(fromNode,instance);
		}
		#endregion
	}
}
