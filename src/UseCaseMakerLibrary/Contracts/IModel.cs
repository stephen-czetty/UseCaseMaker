using System.Xml.Serialization;

namespace UseCaseMakerLibrary.Contracts
{
    /// <summary>
    /// Model interface.  Aggregate root for all other items
    /// </summary>
    public interface IModel : IPackage
    {
        /// <summary>
        /// Gets the glossary.
        /// </summary>
        [XmlArray]
        [XmlArrayItem("GlossaryItem")]
        GlossaryItems Glossary { get; }

        /// <summary>
        /// Creates a new glossary item
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="id">The id.</param>
        /// <returns>The created item</returns>
        GlossaryItem NewGlossaryItem(string name, string prefix, int id);

        /// <summary>
        /// Adds a glossary item.
        /// </summary>
        /// <param name="gi">The glossary item to add.</param>
        void AddGlossaryItem(GlossaryItem gi);

        /// <summary>
        /// Removes the glossary item.
        /// </summary>
        /// <param name="gi">The glossary item to remove</param>
        /// <param name="oldNameStartTag">The old name start tag.</param>
        /// <param name="oldNameEndTag">The old name end tag.</param>
        /// <param name="newNameStartTag">The new name start tag.</param>
        /// <param name="newNameEndTag">The new name end tag.</param>
        /// <param name="doNotMarkOccurrences">if set to <c>true</c> do not mark occurrences.</param>
        void RemoveGlossaryItem(
            GlossaryItem gi,
            string oldNameStartTag,
            string oldNameEndTag,
            string newNameStartTag,
            string newNameEndTag,
            bool doNotMarkOccurrences);

        /// <summary>
        /// Gets a glossary item by unique id.
        /// </summary>
        /// <param name="uniqueId">The unique ID.</param>
        /// <returns>The element, or <c>null</c> if not found.</returns>
        GlossaryItem GetGlossaryItem(string uniqueId);

        /// <summary>
        /// Finds the element by unique ID.
        /// </summary>
        /// <param name="uniqueId">The unique ID.</param>
        /// <returns>The element, or <c>null</c> if not found.</returns>
        IIdentificableObject FindElementByUniqueId(string uniqueId);

        /// <summary>
        /// Finds the element by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The element, or <c>null</c> if not found.</returns>
        IIdentificableObject FindElementByName(string name);

        /// <summary>
        /// Finds the element by path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The element, or <c>null</c> if not found.</returns>
        IIdentificableObject FindElementByPath(string path);
    }
}