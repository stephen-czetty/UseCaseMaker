using System.Linq;
using System.Xml.Serialization;
using UseCaseMakerLibrary.Contracts;

namespace UseCaseMakerLibrary
{
    /// <summary>
    /// Represents a model 
    /// </summary>
    public class Model : Package, IModel
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> class.
        /// </summary>
        public Model()
        {
            this.Glossary = new GlossaryItems(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="id">The id.</param>
        public Model(string name, string prefix, int id)
            : base(name, prefix, id)
        {
            this.Glossary = new GlossaryItems(this);
        }

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the glossary.
        /// </summary>
        [XmlArray]
        [XmlArrayItem("GlossaryItem")]
        public GlossaryItems Glossary { get; private set; }

        #endregion

        #region Public Methods
        #region Glossary Handling
        /// <summary>
        /// Creates a new glossary item
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="id">The id.</param>
        /// <returns>
        /// The created item
        /// </returns>
        public GlossaryItem NewGlossaryItem(string name, string prefix, int id)
        {
            return new GlossaryItem(name, prefix, id, this);
        }

        /// <summary>
        /// Adds a glossary item.
        /// </summary>
        /// <param name="gi">The glossary item to add.</param>
        public void AddGlossaryItem(GlossaryItem gi)
        {
            gi.Owner = this;
            this.Glossary.Add(gi);
        }

        /// <summary>
        /// Removes the glossary item.
        /// </summary>
        /// <param name="gi">The glossary item to remove</param>
        /// <param name="oldNameStartTag">The old name start tag.</param>
        /// <param name="oldNameEndTag">The old name end tag.</param>
        /// <param name="newNameStartTag">The new name start tag.</param>
        /// <param name="newNameEndTag">The new name end tag.</param>
        /// <param name="doNotMarkOccurrences">if set to <c>true</c> do not mark occurrences.</param>
        public void RemoveGlossaryItem(
            GlossaryItem gi,
            string oldNameStartTag,
            string oldNameEndTag,
            string newNameStartTag,
            string newNameEndTag,
            bool doNotMarkOccurrences)
        {
            if (!doNotMarkOccurrences)
            {
                this.ChangeReferences(
                    gi.Name,
                    oldNameStartTag,
                    oldNameEndTag,
                    gi.Name,
                    newNameStartTag,
                    newNameEndTag,
                    false);
            }

            foreach (GlossaryItem tmpGi in this.Glossary.Where(tmpGi => tmpGi.Id > gi.Id))
                tmpGi.Id -= 1;

            this.Glossary.Remove(gi);
        }

        /// <summary>
        /// Gets a glossary item by unique id.
        /// </summary>
        /// <param name="uniqueId">The unique ID.</param>
        /// <returns>
        /// The element, or <c>null</c> if not found.
        /// </returns>
        public GlossaryItem GetGlossaryItem(string uniqueId)
        {
            return Glossary.FindByUniqueId(uniqueId);
        }
        #endregion // Packages Handling

        /// <summary>
        /// Finds the element by unique ID.
        /// </summary>
        /// <param name="uniqueId">The unique ID.</param>
        /// <returns>
        /// The element, or <c>null</c> if not found.
        /// </returns>
        public IIdentificableObject FindElementByUniqueId(string uniqueId)
        {
            if (this.UniqueId == uniqueId)
                return this;

            IIdentificableObject element = this.Glossary.FindByUniqueId(uniqueId);
            if (element != null)
                return element;

            if (this.Requirements.UniqueId == uniqueId)
                return this.Requirements;

            if (this.Actors.UniqueId == uniqueId)
                return this.Actors;

            element = this.Actors.FindByUniqueId(uniqueId);
            if (element != null)
                return element;

            if (this.UseCases.UniqueId == uniqueId)
                return this.UseCases;

            element = this.UseCases.FindByUniqueId(uniqueId);
            return element ?? this.Packages.FindElementByUniqueID(uniqueId);
        }

        /// <summary>
        /// Finds the element by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// The element, or <c>null</c> if not found.
        /// </returns>
        public IIdentificableObject FindElementByName(string name)
        {
            if (this.Name == name)
                return this;

            IIdentificableObject element = this.Glossary.FindByName(name);
            if (element != null)
                return element;

            if (this.Actors.Name == name)
                return this.Actors;

            element = this.Actors.FindByName(name);
            if (element != null)
                return element;

            if (this.UseCases.Name == name)
                return this.UseCases;

            element = this.UseCases.FindByName(name);
            return element ?? this.Packages.FindElementByName(name);
        }

        /// <summary>
        /// Finds the element by path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>
        /// The element, or <c>null</c> if not found.
        /// </returns>
        public IIdentificableObject FindElementByPath(string path)
        {
            if (this.Path == path)
                return this;

            IIdentificableObject element = this.Glossary.FindByPath(path);
            if (element != null)
                return element;

            if (this.Actors.Path == path)
                return this.Actors;

            element = this.Actors.FindByPath(path);
            if (element != null)
                return element;

            if (this.UseCases.Path == path)
                return this.UseCases;

            element = this.UseCases.FindByPath(path);
            return element ?? this.Packages.FindElementByPath(path);
        }
        #endregion
    }
}
