namespace UseCaseMakerLibrary
{
	public class UseCases : IdentificableObjectCollection<UseCase>
	{
		internal UseCases(Package owner)
		{
			Owner = owner;
		}
	}
}
