using System;

namespace UseCaseMakerLibrary
{
    public class Requirement : IdentificableObject
    {
        #region Public Constants and Enumerators
        #endregion

        #region Class Members

		#endregion

        #region Constructors
        internal Requirement()
        {
		    Description = String.Empty;
        }

		#endregion

        #region Public Properties

	    public string Description { get; set; }
        #endregion
    }
}
