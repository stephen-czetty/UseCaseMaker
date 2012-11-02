using System;
using System.Xml;

namespace UseCaseMakerLibrary
{
	/**
	 * @brief Descrizione di riepilogo per GlossaryItem.
	 */
	public class GlossaryItem : IdentificableObject
	{
		#region Class Members

	    #endregion

		#region Constructors
		internal GlossaryItem()
			: base()
		{
		    Description = String.Empty;
		}

	    internal GlossaryItem(String name, String prefix, Int32 id, Package owner)
			: base(name,prefix,id,owner)
	    {
	        Description = String.Empty;
	    }

	    #endregion

		#region Public Properties

	    public string Description { get; set; }

	    #endregion
	}
}
