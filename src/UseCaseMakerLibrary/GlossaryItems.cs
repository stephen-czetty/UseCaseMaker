namespace UseCaseMakerLibrary
{
	public class GlossaryItems : IdentificableObjectCollection<GlossaryItem>
	{
		internal GlossaryItems(Package owner)
		{
			Owner = owner;
		}
	}
}
