using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace UseCaseMaker
{
	/// <summary>
	/// Descrizione di riepilogo per frmOptions.
	/// </summary>
	public class frmOptions : System.Windows.Forms.Form
	{
		private enum FlagsIndex
		{
			NC = 0,
			FR = 1,
			DE = 2,
			IT = 3,
			PT = 4,
			ES = 5,
			EN = 6,
			JP = 7
		}

		private System.Windows.Forms.TabControl tabOptions;
		private System.Windows.Forms.TabPage pgOptLanguages;
		private System.Windows.Forms.Label lblSelectLanguageTitle;
		private System.Windows.Forms.ListView lvOptLanguages;
		private System.Windows.Forms.ColumnHeader chFlag;
		private System.Windows.Forms.ColumnHeader chLanguage;
		private System.Windows.Forms.ImageList imgFlagList;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.ComponentModel.IContainer components;

		public string SelectedLanguage = string.Empty;

		public frmOptions(string [] availableLanguages, string actualLanguage, Localizer localizer)
		{
			//
			// Necessario per il supporto di Progettazione Windows Form
			//
			InitializeComponent();

			//
			// TODO: aggiungere il codice del costruttore dopo la chiamata a InitializeComponent
			//
			foreach(string lang in availableLanguages)
			{
				ListViewItem lviFlag = new ListViewItem();
				try
				{
					lviFlag.StateImageIndex = (int)Enum.Parse(typeof(FlagsIndex),lang,true);
				}
				catch(ArgumentException)
				{
					lviFlag.StateImageIndex = (int)FlagsIndex.NC;
				}
				lviFlag.SubItems.Add(lang.ToUpper());
				lvOptLanguages.Items.Add(lviFlag);
			};

			foreach(ListViewItem lvi in lvOptLanguages.Items)
			{
				if(lvi.SubItems[1].Text == actualLanguage)
				{
					lvi.Selected = true;
					break;
				}
			}

			localizer.LocalizeControls(this);
		}

		/// <summary>
		/// Pulire le risorse in uso.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Codice generato da Progettazione Windows Form
		/// <summary>
		/// Metodo necessario per il supporto della finestra di progettazione. Non modificare
		/// il contenuto del metodo con l'editor di codice.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmOptions));
			this.tabOptions = new System.Windows.Forms.TabControl();
			this.pgOptLanguages = new System.Windows.Forms.TabPage();
			this.lvOptLanguages = new System.Windows.Forms.ListView();
			this.chFlag = new System.Windows.Forms.ColumnHeader();
			this.chLanguage = new System.Windows.Forms.ColumnHeader();
			this.imgFlagList = new System.Windows.Forms.ImageList(this.components);
			this.lblSelectLanguageTitle = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.tabOptions.SuspendLayout();
			this.pgOptLanguages.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabOptions
			// 
			this.tabOptions.Controls.Add(this.pgOptLanguages);
			this.tabOptions.Location = new System.Drawing.Point(8, 8);
			this.tabOptions.Name = "tabOptions";
			this.tabOptions.SelectedIndex = 0;
			this.tabOptions.Size = new System.Drawing.Size(424, 240);
			this.tabOptions.TabIndex = 0;
			// 
			// pgOptLanguages
			// 
			this.pgOptLanguages.Controls.Add(this.lvOptLanguages);
			this.pgOptLanguages.Controls.Add(this.lblSelectLanguageTitle);
			this.pgOptLanguages.Location = new System.Drawing.Point(4, 22);
			this.pgOptLanguages.Name = "pgOptLanguages";
			this.pgOptLanguages.Size = new System.Drawing.Size(416, 214);
			this.pgOptLanguages.TabIndex = 0;
			this.pgOptLanguages.Text = "[Available languages]";
			// 
			// lvOptLanguages
			// 
			this.lvOptLanguages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							 this.chFlag,
																							 this.chLanguage});
			this.lvOptLanguages.FullRowSelect = true;
			this.lvOptLanguages.GridLines = true;
			this.lvOptLanguages.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lvOptLanguages.HideSelection = false;
			this.lvOptLanguages.LargeImageList = this.imgFlagList;
			this.lvOptLanguages.Location = new System.Drawing.Point(8, 32);
			this.lvOptLanguages.Name = "lvOptLanguages";
			this.lvOptLanguages.Size = new System.Drawing.Size(400, 176);
			this.lvOptLanguages.SmallImageList = this.imgFlagList;
			this.lvOptLanguages.StateImageList = this.imgFlagList;
			this.lvOptLanguages.TabIndex = 1;
			this.lvOptLanguages.View = System.Windows.Forms.View.Details;
			this.lvOptLanguages.SelectedIndexChanged += new System.EventHandler(this.lvOptLanguages_SelectedIndexChanged);
			// 
			// chFlag
			// 
			this.chFlag.Width = 36;
			// 
			// chLanguage
			// 
			this.chLanguage.Width = 360;
			// 
			// imgFlagList
			// 
			this.imgFlagList.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imgFlagList.ImageSize = new System.Drawing.Size(32, 32);
			this.imgFlagList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgFlagList.ImageStream")));
			this.imgFlagList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// lblSelectLanguageTitle
			// 
			this.lblSelectLanguageTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblSelectLanguageTitle.Location = new System.Drawing.Point(8, 8);
			this.lblSelectLanguageTitle.Name = "lblSelectLanguageTitle";
			this.lblSelectLanguageTitle.Size = new System.Drawing.Size(400, 16);
			this.lblSelectLanguageTitle.TabIndex = 0;
			this.lblSelectLanguageTitle.Text = "[Select the user interface language]";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.CausesValidation = false;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(224, 256);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(120, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "[Cancel]";
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(96, 256);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(120, 23);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "[OK]";
			// 
			// frmOptions
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(440, 288);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.tabOptions);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmOptions";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "[Options]";
			this.tabOptions.ResumeLayout(false);
			this.pgOptLanguages.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void lvOptLanguages_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(lvOptLanguages.SelectedIndices.Count == 0)
			{
				this.SelectedLanguage = string.Empty;
				btnOK.Enabled = false;
			}
			else
			{
				this.SelectedLanguage = lvOptLanguages.SelectedItems[0].SubItems[1].Text;
				btnOK.Enabled = true;
			}
		}
	}
}
