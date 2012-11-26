using System.Windows.Forms;
using System.Xml.XPath;

namespace UseCaseMakerLibrary.Contracts
{
    /// <summary>
    /// Service for getting localized strings
    /// </summary>
    public interface ILocalizationService
    {
        /// <summary>
        /// Loads the specified language file path.
        /// </summary>
        /// <param name="languageFilePath">The language file path.</param>
        void Load(string languageFilePath);

        /// <summary>
        /// Gets a localized string.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="name">The name.</param>
        /// <returns>The localized value</returns>
        string GetValue(string section, string name);

        /// <summary>
        /// Gets the node set.
        /// </summary>
        /// <param name="parentName">Name of the parent.</param>
        /// <param name="childsName">Name of the child.</param>
        /// <returns>The node set</returns>
        XPathNodeIterator GetNodeSet(string parentName, string childsName);

        /// <summary>
        /// Localizes the controls.
        /// </summary>
        /// <param name="form">The form.</param>
        void LocalizeControls(Form form);
    }
}