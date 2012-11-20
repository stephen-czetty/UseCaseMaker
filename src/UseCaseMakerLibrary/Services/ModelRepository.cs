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
        /// Initializes a new instance of the <see cref="ModelRepository"/> class.
        /// </summary>
        /// <param name="localizationService">The localization service.</param>
        public ModelRepository(ILocalizationService localizationService)
        {
            this._localizationService = localizationService;
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
    }
}