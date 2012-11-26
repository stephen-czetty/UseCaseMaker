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

        /// <summary>
        /// Loads the model.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The loaded model</returns>
        IModel LoadModel(string fileName);

        /// <summary>
        /// Saves the model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="fileName">Name of the file.</param>
        void SaveModel(IModel model, string fileName);
    }
}