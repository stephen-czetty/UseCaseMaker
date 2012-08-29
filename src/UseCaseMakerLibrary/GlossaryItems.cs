using System;

namespace UseCaseMakerLibrary
{
	/**
	 * @brief Descrizione di riepilogo per GlossaryItems.
	 */
	public class GlossaryItems : IdentificableObjectCollection<GlossaryItem>
	{
		internal GlossaryItems(Package owner)
		{
			base.Owner = owner;
		}
	}
}
