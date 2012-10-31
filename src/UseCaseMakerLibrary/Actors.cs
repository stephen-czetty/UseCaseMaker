namespace UseCaseMakerLibrary
{
	public class Actors : IdentificableObjectCollection<Actor>
	{
		internal Actors(Package owner)
		{
			Owner = owner;
		}
	}
}
