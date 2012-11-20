using System;
using System.Diagnostics.Contracts;

namespace UseCaseMakerLibrary.Contracts
{
    /// <summary>
    /// Service for loading and saving data
    /// </summary>
    [ContractClass(typeof(SavedDataServiceContract))]
    public interface ISavedDataService
    {
        /// <summary>
        /// Loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The loaded <see cref="Model"/></returns>
        IModel Load(string fileName);

        /// <summary>
        /// Saves the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="fileName">Name of the file.</param>
        void Save(IModel model, string fileName);
    }

    /// <summary>
    /// Contract for <see cref="ISavedDataService"/>
    /// </summary>
    [ContractClassFor(typeof(ISavedDataService))]
    internal abstract class SavedDataServiceContract : ISavedDataService
    {
        /// <summary>
        /// Loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The loaded <see cref="Model"/></returns>
        public IModel Load(string fileName)
        {
            Contract.Requires<ArgumentNullException>(fileName != null);
            Contract.Requires<ArgumentException>(fileName.Length > 0);
            Contract.Ensures(Contract.Result<IModel>() != null);
            return default(Model);
        }

        /// <summary>
        /// Saves the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="fileName">Name of the file.</param>
        public void Save(IModel model, string fileName)
        {
            Contract.Requires<ArgumentNullException>(model != null);
            Contract.Requires<ArgumentNullException>(fileName != null);
            Contract.Requires<ArgumentException>(fileName.Length > 0);
        }
    }
}