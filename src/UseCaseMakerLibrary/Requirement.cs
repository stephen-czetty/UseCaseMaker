using System;

namespace UseCaseMakerLibrary
{
    public class Requirement : IdentificableObject
    {
        #region Public Constants and Enumerators
        #endregion

        #region Class Members
        private String description = String.Empty;
        #endregion

        #region Constructors
        internal Requirement()
        {
        }
        #endregion

        #region Public Properties
        public String Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }
        #endregion
    }
}
