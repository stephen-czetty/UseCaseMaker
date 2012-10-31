using System;
using System.Xml;
using System.Globalization;
using System.Xml.Serialization;

namespace UseCaseMakerLibrary
{
	/// <summary>
	/// Descrizione di riepilogo per HistoryItem.
	/// </summary>
	public class HistoryItem : IXMLNodeSerializable
	{
		#region Public Constants and Enumerators
		public enum HistoryType
		{
			Implementation = 0,
			Status = 1
		}
		#endregion

		#region Class Members

	    #endregion

		#region Constructors
		public HistoryItem()
		{
		    Notes = String.Empty;
		}

	    #endregion

		#region Public Properties
        [XmlIgnore]
		public String LocalizatedDateValue
		{
			get
			{
				return Date.ToString("d",DateTimeFormatInfo.CurrentInfo);
			}
		}

        [XmlIgnore]
	    public DateTime Date { get; set; }

	    public String DateValue
		{
			get
			{
				return Convert.ToString(this.Date,DateTimeFormatInfo.InvariantInfo);
			}
			set
			{
				this.Date = Convert.ToDateTime(value,DateTimeFormatInfo.InvariantInfo);
			}
		}

	    public HistoryType Type { get; set; }

	    public int Action { get; set; }

	    public string Notes { get; set; }

	    #endregion
    }
}
