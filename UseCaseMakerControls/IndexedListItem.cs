using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace UseCaseMakerControls
{
	public delegate void MouseOverTokenEventHandler(object sender, MouseOverTokenEventArgs e);
	public delegate void SelectedChangeEventHandler(object sender, EventArgs e);
	public delegate void ItemTextChangedEventHandler(object sender, EventArgs e);
	public delegate void ClickOnTokenEventHandler(object sender, MouseOverTokenEventArgs e);

	/// <summary>
	/// Descrizione di riepilogo per IndexedListItem.
	/// </summary>
	public class IndexedListItem
	{
		#region Private Enumerators and Constants
		#endregion

		#region Public Enumerators and Constants
		#endregion

		#region Class Members
		private Label								_index = new Label();
		private LinkEnabledRTB						_text = new LinkEnabledRTB();
		private bool								_selected = false;
		private object								_tag = null;

		public event MouseOverTokenEventHandler		MouseOverToken;
		public event SelectedChangeEventHandler		SelectedChange;
		public event ItemTextChangedEventHandler	ItemTextChanged;
		public event ClickOnTokenEventHandler		ClickOnToken;
		#endregion

		#region Constructors
		public IndexedListItem()
		{
			_text.MouseOverToken += new MouseOverTokenEventHandler(_text_MouseOverToken);
			_text.ItemTextChanged += new ItemTextChangedEventHandler(_text_ItemTextChanged);
			_text.ClickOnToken += new ClickOnTokenEventHandler(_text_ClickOnToken);
		}
		#endregion

		#region Events
		#endregion

		#region Public Properties
		[ 
		Category("Default"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
		NotifyParentProperty(true),
		Bindable(true)
		]
		public String Index
		{
			get
			{
				return this._index.Text;
			}
			set
			{
				this._index.Text = value;
			}
		}

		[ 
		Category("Default"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
		NotifyParentProperty(true),
		Bindable(true)
		]
		public String Text
		{
			get
			{
				return this._text.Text;
			}
			set
			{
				this._text.Text = value;
			}
		}

		[ 
		Category("Default"),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
		NotifyParentProperty(true)
		]
		public object Tag
		{
			get
			{
				return this._tag;
			}
			set
			{
				this._tag = value;
			}
		}

		[ 
		Browsable(false)
		]
		internal Label ItemLabel
		{
			get
			{
				return this._index;
			}
		}

		[ 
		Browsable(false)
		]
		internal LinkEnabledRTB ItemRichTextBox
		{
			get
			{
				return this._text;
			}
		}

		[ 
		Browsable(false)
		]
		public bool Selected
		{
			get
			{
				return this._selected;
			}
			set
			{
				this._selected = value;
				if(this._selected && SelectedChange != null)
				{
					SelectedChange(this.ItemLabel, new EventArgs());
				}
			}
		}
		#endregion

		#region Public Methods
		#endregion

		#region Protected Methods
		protected void _text_MouseOverToken(object sender, MouseOverTokenEventArgs e)
		{
			if(MouseOverToken != null)
			{
				MouseOverToken(sender,e);
			}
		}

		protected void _text_ItemTextChanged(object sender, EventArgs e)
		{
			if(ItemTextChanged != null)
			{
				ItemTextChanged(sender,e);
			}
		}

		protected void _text_ClickOnToken(object sender, MouseOverTokenEventArgs e)
		{
			if(ClickOnToken != null)
			{
				ClickOnToken(sender,e);
			}
		}
		#endregion

		#region Private Methods
		#endregion
	}
}