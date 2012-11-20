namespace UseCaseMakerLibrary.Contracts
{
    /// <summary>
    /// Model Repository
    /// </summary>
    public interface IModelRepository
    {
        /// <summary>
        /// Creates the new model.
        /// </summary>
        /// <returns>The created model.</returns>
        IModel CreateNewModel();
    }
}