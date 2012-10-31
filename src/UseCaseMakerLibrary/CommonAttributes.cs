using System;
using System.Xml.Serialization;

namespace UseCaseMakerLibrary
{
	public class CommonAttributes : IXMLNodeSerializable
	{
		#region Class Members

	    public CommonAttributes()
	    {
	        RelatedDocuments = new RelatedDocuments();
	        Notes = String.Empty;
	        Description = String.Empty;
	    }

	    #endregion

		#region Constructors
		#endregion

		#region Public Properties

	    public string Description { get; set; }

	    public string Notes { get; set; }

        [XmlArray]
        [XmlArrayItem("RelatedDocument")]
	    public RelatedDocuments RelatedDocuments { get; private set; }

	    #endregion

		}

}
