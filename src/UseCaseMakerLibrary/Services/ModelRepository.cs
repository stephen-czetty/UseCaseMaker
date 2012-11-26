using UseCaseMakerLibrary.Contracts;

namespace UseCaseMakerLibrary.Services
{
    /// <summary>
    /// Service for creating models
    /// </summary>
    public class ModelRepository : IModelRepository
    {
        /// <summary>
        /// The default prefix for models
        /// </summary>
        private const string DefaultModelPrefix = "M";

        /// <summary>
        /// Service for getting localized strings
        /// </summary>
        private readonly ILocalizationService _localizationService;

        /// <summary>
        /// Service for loading and saving data
        /// </summary>
        private readonly ISavedDataService _savedDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelRepository"/> class.
        /// </summary>
        /// <param name="localizationService">The localization service.</param>
        /// <param name="savedDataService">The saved data service.</param>
        public ModelRepository(ILocalizationService localizationService, ISavedDataService savedDataService)
        {
            _localizationService = localizationService;
            _savedDataService = savedDataService;
        }

        /// <summary>
        /// Creates a new model.
        /// </summary>
        /// <returns>
        /// The created model.
        /// </returns>
        public IModel CreateNewModel()
        {
            return new Model(_localizationService.GetValue("Globals", "NewModel"), DefaultModelPrefix, 1);
        }

        /// <summary>
        /// Loads the model.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The loaded model</returns>
        public IModel LoadModel(string fileName)
        {
            return _savedDataService.Load(fileName);
        }

        /// <summary>
        /// Saves the model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="fileName">Name of the file.</param>
        public void SaveModel(IModel model, string fileName)
        {
            _savedDataService.Save(model, fileName);
        }
    }
}