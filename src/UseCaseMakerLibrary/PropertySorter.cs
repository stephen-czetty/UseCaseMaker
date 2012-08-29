using System;
using System.Collections.Generic;

namespace UseCaseMakerLibrary
{
	/// <summary>
	/// Descrizione di riepilogo per GenericSort.
	/// </summary>
	public class PropertySorter<T> : IComparer<T>
	{
	    readonly String sortPropertyName;
	    readonly String sortOrder;

		public PropertySorter(string sortPropertyName, string sortOrder) 
		{
			this.sortPropertyName = sortPropertyName;
			this.sortOrder = sortOrder;
		}

	    #region Implementation of IComparer<in T>

	    public int Compare(T x, T y)
	    {
            var ic1 = (IComparable)x.GetType().GetProperty(sortPropertyName).GetValue(x, null);
            var ic2 = (IComparable)y.GetType().GetProperty(sortPropertyName).GetValue(y, null);

            if (sortOrder != null && sortOrder.ToUpper().Equals("ASC"))
            {
                return ic1.CompareTo(ic2);
            }
            else
            {
                return ic2.CompareTo(ic1);
            }
	    }

	    #endregion
	}
}
