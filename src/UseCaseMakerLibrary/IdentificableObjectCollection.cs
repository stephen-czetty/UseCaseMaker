using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Xml.Serialization;
using UseCaseMakerLibrary.Contracts;

namespace UseCaseMakerLibrary
{
    /// <summary>
    /// Collection of <see cref="IIdentificableObject"/>
    /// </summary>
    /// <typeparam name="T">An <see cref="IIdentificableObject"/></typeparam>
    public abstract class IdentificableObjectCollection<T> : IdentificableObject, ICollection<T>, ICollection
        where T : class, IIdentificableObject
    {
        /// <summary>
        /// Items of <typeparamref name="T" />
        /// </summary>
        private readonly IList<T> _items = new List<T>();

        /// <summary>
        /// Object used for locking
        /// </summary>
        private readonly object _syncRoot = new object();

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

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.
        /// </summary>
        /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only; otherwise, false.</returns>
        [XmlIgnore]
        public bool IsReadOnly
        {
            get { return _items.IsReadOnly; }
        }

        /// <summary>
        /// Gets the path.
        /// </summary>
        [XmlIgnore]
        public override string Path
        {
            get
            {
                if (Owner == null)
                    return string.Empty;

                return Owner.Path;
            }
        }

        /// <summary>
        /// Gets the element id.
        /// </summary>
        [XmlIgnore]
        public override string ElementId
        {
            get
            {
                return Owner.ElementId;
            }
        }

        /// <summary>
        /// Gets the number of elements in the MenuItemCollection
        /// </summary>
        [XmlIgnore]
        public int Count
        {
            get
            {
                return this._items.Count;
            }
        }

        /// <summary>
        /// Gets the <see cref="T"/> at the specified index.
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>The item at the specified index</returns>
        [XmlIgnore]
        public T this[int index]
        {
            get
            {
                return this._items[index];
            }
        }

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.ICollection"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection"/>. The <see cref="T:System.Array"/> must have zero-based indexing. </param><param name="index">The zero-based index in <paramref name="array"/> at which copying begins. </param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero. </exception><exception cref="T:System.ArgumentException"><paramref name="array"/> is multidimensional.-or- The number of elements in the source <see cref="T:System.Collections.ICollection"/> is greater than the available space from <paramref name="index"/> to the end of the destination <paramref name="array"/>.-or-The type of the source <see cref="T:System.Collections.ICollection"/> cannot be cast automatically to the type of the destination <paramref name="array"/>.</exception><filterpriority>2</filterpriority>
        void ICollection.CopyTo(Array array, int index)
        {
            Contract.Requires<ArgumentNullException>(array != null);
            Contract.Requires<ArgumentOutOfRangeException>(index >= 0);
            if (array.Rank > 1 || array.Length < this._items.Count + index)
                throw new ArgumentException("array");

            foreach (T item in this._items)
                array.SetValue(item, index++);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <returns>
        /// true if item was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false. This method also returns false if item is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.</exception>
        public bool Remove(T item)
        {
            return this._items.Remove(item);
        }

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.</exception>
        public void Add(T item)
        {
             _items.Add(item);
        }

        /// <summary>
        /// Inserts the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="item">The item.</param>
        public void Insert(int index, T item)
        {
            _items.Insert(index, item);
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only. </exception>
        public void Clear()
        {
            _items.Clear();
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <returns>
        /// true if item is found in the <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false.
        /// </returns>
        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        /// <summary>
        /// Copies to the specified array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="arrayIndex">Index of the array.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Returns a list sorted by the property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>A sorted list.</returns>
        public IList<T> Sorted(string propertyName)
        {
            propertyName = propertyName == "ID" ? "Id" : propertyName;
            var ps = new PropertySorter<T>(propertyName, "ASC");
            return _items.OrderBy(x => x, ps).ToList();
        }

        /// <summary>
        /// Finds by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The found object.</returns>
        public IIdentificableObject FindByName(string name)
        {
            IIdentificableObject element = null;
            foreach (T tmpElement in this.Where(tmpElement => tmpElement.Name == name))
                element = tmpElement;

            return element;
        }

        /// <summary>
        /// Finds the by unique id.
        /// </summary>
        /// <param name="uniqueId">The unique ID.</param>
        /// <returns>The found object.</returns>
        public T FindByUniqueId(string uniqueId)
        {
            T element = null;
            foreach (T tmpElement in this.Where(tmpElement => tmpElement.UniqueId == uniqueId))
                element = tmpElement;

            return element;
        }

        /// <summary>
        /// Finds the by path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The found object.</returns>
        public T FindByPath(string path)
        {
            T element = null;
            foreach (T tmpElement in this._items.Where(tmpElement => tmpElement.Path == path))
                element = tmpElement;

            return element;
        }

        /// <summary>
        /// Gets the next free id.
        /// </summary>
        /// <returns>The next free id.</returns>
        public int GetNextFreeId()
        {
            int id = this._items.Select(tmpElement => tmpElement.Id).Concat(new[] { 0 }).Max();

            return id + 1;
        }
    }
}
