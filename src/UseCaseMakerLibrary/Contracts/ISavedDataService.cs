using System;
using System.Diagnostics.Contracts;

namespace UseCaseMakerLibrary.Contracts
{
    [ContractClass(typeof(SavedDataServiceContract))]
    public interface ISavedDataService
    {
        /// <summary>
        /// Loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        Model Load(string fileName);

        /// <summary>
        /// Saves the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="fileName">Name of the file.</param>
        void Save(Model model, string fileName);
    }

    [ContractClassFor(typeof(ISavedDataService))]
    internal abstract class SavedDataServiceContract : ISavedDataService
    {
        public Model Load(string fileName)
        {
            Contract.Requires<ArgumentNullException>(fileName != null);
            Contract.Requires<ArgumentException>(fileName.Length > 0);
            Contract.Ensures(Contract.Result<Model>() != null);
            return default(Model);
        }

        public void Save(Model model, string fileName)
        {
            Contract.Requires<ArgumentNullException>(model != null);
            Contract.Requires<ArgumentNullException>(fileName != null);
            Contract.Requires<ArgumentException>(fileName.Length > 0);
        }
    }
}