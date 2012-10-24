using System;
using System.Windows.Forms;

namespace UseCaseMakerLibrary
{
	public class UserViewStatus
	{
		#region Class Members

	    #endregion

		#region Constructors
		public UserViewStatus()
		{
		    CurrentTabPage = null;
		}

	    #endregion

		#region Public Properties

	    public TabPage CurrentTabPage { get; set; }

	    #endregion
	}
}
