namespace UseCaseMakerLibrary.Contracts
{
    /// <summary>
    /// An object that has properties that can be used for identification
    /// </summary>
    public interface IIdentificableObject
    {
        /// <summary>
        /// Gets the unique ID.
        /// </summary>
        string UniqueId
        {
            get;
        }

        /// <summary>
        /// Gets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        Package Owner
        {
            get;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name
        {
            get;
        }

        /// <summary>
        /// Gets the Id.
        /// </summary>
        /// <value>
        /// The Id.
        /// </value>
        int Id
        {
            get;
        }

        /// <summary>
        /// Gets the prefix.
        /// </summary>
        /// <value>
        /// The prefix.
        /// </value>
        string Prefix
        {
            get;
        }

        /// <summary>
        /// Gets the path.
        /// </summary>
        string Path
        {
            get;
        }

        /// <summary>
        /// Gets the element ID.
        /// </summary>
        string ElementId
        {
            get;
        }

        /// <summary>
        /// Purges the references.
        /// </summary>
        /// <param name="thisPackage">The this package.</param>
        /// <param name="currentPackage">The current package.</param>
        /// <param name="oldNameStartTag">The old name start tag.</param>
        /// <param name="oldNameEndTag">The old name end tag.</param>
        /// <param name="newNameStartTag">The new name start tag.</param>
        /// <param name="newNameEndTag">The new name end tag.</param>
        /// <param name="doNotMark">if set to <c>true</c> [do not mark].</param>
        //// TODO: This is not ideal, target for refactoring later.
        void PurgeReferences(Package thisPackage, Package currentPackage, string oldNameStartTag, string oldNameEndTag, string newNameStartTag, string newNameEndTag, bool doNotMark);
    }
}
